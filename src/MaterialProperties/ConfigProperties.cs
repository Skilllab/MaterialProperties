namespace Material
{
    /// <summary>
    ///     Класс материала "Настройка"
    /// </summary>
    public class ConfigProperties
    {
        public ConfigProperties(string name, string descr, string val, string units)
        {
            PropertyName = name ?? string.Empty;
            PropertyDescription = descr ?? string.Empty;
            PropertyValue = val ?? string.Empty;
            PropertyUnits = units ?? string.Empty;
        }

        public string PropertyDescription { get; }
        public string PropertyName { get; }
        public string PropertyUnits { get; }
        public string PropertyValue { get; }
    }
}