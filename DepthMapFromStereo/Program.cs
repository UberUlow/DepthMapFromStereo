using System;
using System.Collections.Generic;

namespace DepthMapFromStereo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Построение карты глубины на основе данных, полученных при помощи стереопары.\n\n");
            CreateDepthMap();
            Console.ReadKey();
        }

        private static void CreateDepthMap()
        {
            List<Pair> pairs = ImportXML();
            DepthMap dm = new DepthMap();
            dm.Create(pairs);
        }

        /// <summary>
        /// Импорт XML-файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        private static List<Pair> ImportXML()
        {
            Console.WriteLine("Введите имя XML-файла (*.xml):");
            List<Pair> pairs = new List<Pair>();
            bool flag = false;
            while (!flag)
            {
                pairs = XML.Import<Pair>(Console.ReadLine() + ".xml");
                if (pairs != null)
                {
                    flag = true;
                }
            }
            return pairs;
        }
    }
}
