namespace DepthMapFromStereo
{
    /// <summary>
    /// Класс для записи информации о результатах построения карты глубины в Xml-файл
    /// </summary>
    public class Results
    {
        public Pair.Settings Properties;
        public string OutputDepthMapPath;
        public string OutputDepthMapImagePath;

        public Results() { }

        public Results(Pair.Settings properties, string outputDepthMapPath, string outputDepthMapImagePath)
        {
            Properties = properties;
            OutputDepthMapPath = outputDepthMapPath;
            OutputDepthMapImagePath = outputDepthMapImagePath;
        }
    }
}
