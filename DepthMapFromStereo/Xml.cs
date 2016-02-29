using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DepthMapFromStereo
{
    /// <summary>
    /// Класс чтения и записи любых классов в XML-файл
    /// </summary>
    public static class XML
    {
        /// <summary>
        /// Import XML
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Список классов</returns>
        public static List<T> Import<T>(string fileName)
        {
            try
            {
                List<T> objects = new List<T>();
                XmlSerializer srzr = new XmlSerializer(typeof(List<T>));
                StreamReader reader = new StreamReader(fileName);
                objects = (List<T>)srzr.Deserialize(reader);
                reader.Close();
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
        /// Export XML
        /// </summary>
        /// <param name="objects">Список классов</param>
        /// <param name="fileName">Имя файла</param>
        public static void Write<T>(List<T> objects, string fileName)
        {
            XmlSerializer srzr = new XmlSerializer(typeof(List<T>));
            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                srzr.Serialize(sw, objects);
                sw.Close();
                Console.WriteLine("Данные экспортированы в XML-файл\n");
            }
            catch
            {
                Console.WriteLine("Ошибка записи данных в файл.");
            }
        }
    }
}
