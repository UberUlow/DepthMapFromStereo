using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DepthMapFromStereo
{
    /// <summary>
    /// Класс чтения и записи любых классов в Xml-файл
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Import Xml
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Список классов</returns>
        public static List<T> Import<T>(string fileName)
        {
            try
            {
                List<T> objects = new List<T>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    XmlSerializer srzr = new XmlSerializer(typeof(List<T>));
                    objects = (List<T>)srzr.Deserialize(reader);
                }
                Console.WriteLine("Данные импортированы из XML-файла.\n");
                return objects;
            }
            catch
            {
                Console.WriteLine("Ошибка чтения данных из XML-файла. Повторите попытку.");
                return null;
            }
        }

        /// <summary>
        /// Export Xml
        /// </summary>
        /// <param name="objects">Список классов</param>
        /// <param name="fileName">Имя файла</param>
        public static void Export<T>(List<T> objects, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    XmlSerializer srzr = new XmlSerializer(typeof(List<T>));
                    srzr.Serialize(sw, objects);
                }
                Console.WriteLine("Данные экспортированы в XML-файл\n");
            }
            catch
            {
                Console.WriteLine("Ошибка записи данных в файл.");
            }
        }
    }
}
