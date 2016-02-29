using System;
using System.Collections.Generic;

namespace DepthMapFromStereo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя XML-файла (*.xml):");
            List<Pair> pairs = ImportXML();

        }

        /// <summary>
        /// Импорт XML-файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        private static List<Pair> ImportXML()
        {
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
