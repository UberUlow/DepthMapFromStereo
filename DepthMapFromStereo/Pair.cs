namespace DepthMapFromStereo
{
    /// <summary>
    /// Пара изображений с параметрами камеры и размером шаблона
    /// </summary>
    public class Pair
    {
        public Image Image1; // Изображение 1
        public Image Image2; // Изображение 2
        public Settings Properties; // Параметры камеры

        public Pair() { }

        public Pair(Image image1, Image image2, Settings properties)
        {
            Image1 = image1;
            Image2 = image2;
            Properties = properties;
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public class Image
        {
            public string Path; // Путь к файлу
            public Coordinates CoordinatesCamera; // Координаты камеры в момент съемки

            public Image() { }

            public Image(string path, Coordinates coordinatesCamera)
            {
                Path = path;
                CoordinatesCamera = coordinatesCamera;
            }

            /// <summary>
            /// Координаты камеры в момент съемки
            /// </summary>
            public class Coordinates
            {
                public int X; // Координата X
                public int Y; // Координата Y
                public int Z; // Координата Z

                public Coordinates() { }

                public Coordinates(int x, int y, int z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }
            }
        }

        /// <summary>
        /// Параметры камеры и размер шаблона дял поиска
        /// </summary>
        public class Settings
        {
            public double FocalLength; // Фокусное расстояние
            public double Distance; // Расстояние между камерами
            public int TemplateSize; // Размер шаблона

            public Settings() { }

            public Settings(double focalLength, double distance, int templateSize)
            {
                FocalLength = focalLength;
                Distance = distance;
                TemplateSize = templateSize;
            }
        }
    }
}
