using System;
using System.Drawing;
using System.Collections.Generic;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Diagnostics;

namespace DepthMapFromStereo
{
    /// <summary>
    /// Класс для вычисления и создания карты глубины. А также поиск шаблона на втором изображении
    /// </summary>
    public class DepthMap
    {
        private Point objectLocation;

        /// <summary>
        /// Создание карты глубины
        /// </summary>
        /// <param name="pairs">Список пар изображений с параметрами</param>
        public void Create(List<Pair> pairs)
        {
            for (int k = 0; k < pairs.Count; k++)
            {
                Image<Gray, byte> image1 = new Image<Gray, byte>(pairs[k].Image1.Path);
                Image<Gray, byte> image2 = new Image<Gray, byte>(pairs[k].Image2.Path);
                double[,] depthMap = new double[image1.Width, image1.Height];
                Image<Gray, byte> depthMapImg = new Image<Gray, byte>(image1.Width, image1.Height);

                Stopwatch sw = Stopwatch.StartNew();
                Console.WriteLine($"Строится карта глубины для {k + 1}-й пары...");
                for (int i = 0; i < image2.Width; i += pairs[k].Properties.TemplateSize)
                {
                    for (int j = 0; j < image2.Height; j += pairs[k].Properties.TemplateSize)
                    {
                        Rectangle rectangle1 = new Rectangle(i, j, pairs[k].Properties.TemplateSize, pairs[k].Properties.TemplateSize);
                        if (DetectObject(image2, image1.Copy(rectangle1)))
                        {
                            Rectangle rectangle2 = new Rectangle(objectLocation.X, objectLocation.Y, pairs[k].Properties.TemplateSize, pairs[k].Properties.TemplateSize);
                            Calculate(depthMap, depthMapImg, rectangle1, rectangle2, pairs[k].Properties.FocalLength, pairs[k].Properties.Distance);
                        }
                    }
                }
                depthMapImg.Save($"Results/test{k + 1}.jpg");
                Console.WriteLine($"Карта глубины построена записана в файл 'Results/test{k + 1}.jpg'. Это заняло {Math.Round(sw.Elapsed.TotalMilliseconds / 1000, 2)} секунд(ы).\n");
                sw.Stop();
            }
        }

        /// <summary>
        /// Вычисление расстояния до найденного шаблона
        /// </summary>
        /// <param name="depthMap">Значения карты глубины</param>
        /// <param name="depthMapImg">Изображение карты глубины</param>
        /// <param name="rectangle1">Квадрат шаблона 1</param>
        /// <param name="rectangle2">Квадрат шаблона 2</param>
        /// <param name="focalLength">Фокусное расстояние</param>
        /// <param name="distance">Расстояние между камерами</param>
        public void Calculate(double[,] depthMap, Image<Gray, byte> depthMapImg, Rectangle rectangle1, Rectangle rectangle2, double focalLength, double distance)
        {
            int m = 0;
            for (int i = rectangle1.X; i < rectangle1.X + rectangle1.Width; i++)
            {
                for (int j = rectangle1.Y; j < rectangle1.Y + rectangle1.Height; j++)
                {
                    depthMap[i, j] = (focalLength * distance) / (Math.Abs((rectangle1.X + m) - (rectangle2.X + m)));
                    depthMapImg[j, i] = new Gray(depthMap[i, j]);
                }
                m++;
            }
        }

        /// <summary>
        /// Обнаружение объекта
        /// </summary>
        /// <param name="inputImage">Изображение</param>
        /// <param name="objectImage">Изображение объекта</param>
        /// <returns>Результат поиска объекта</returns>
        public bool DetectObject(Image<Gray, byte> inputImage, Image<Gray, byte> objectImage)
        {
            Point dftSize = new Point(inputImage.Width + (objectImage.Width * 2), inputImage.Height + (objectImage.Height * 2));
            bool success = false;

            using (Image<Gray, byte> padArray = new Image<Gray, byte>(dftSize.X, dftSize.Y))
            {
                padArray.ROI = new Rectangle(objectImage.Width, objectImage.Height, inputImage.Width, inputImage.Height);
                CvInvoke.cvCopy(inputImage, padArray, IntPtr.Zero);

                padArray.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));
                using (Image<Gray, float> resultMatrix = padArray.MatchTemplate(objectImage, TemplateMatchingType.CcoeffNormed))
                {
                    resultMatrix.ROI = new Rectangle(objectImage.Width, objectImage.Height, inputImage.Width, inputImage.Height);
                    Point[] maxLoc, minLoc;
                    double[] min, max;
                    resultMatrix.MinMax(out min, out max, out minLoc, out maxLoc);

                    using (Image<Gray, double> RG_Image = resultMatrix.Convert<Gray, double>())
                    {
                        if (max[0] > 0.4)
                        {
                            objectLocation = maxLoc[0];
                            success = true;
                        }
                    }
                }
            }
            return success;
        }
    }
}
