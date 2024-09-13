namespace ReportService.Core.Helpers
{
    public class PropertyData
    {
        /// <summary>
        /// Название столбца
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Признак объединения ячеек
        /// </summary>
        public bool IsMerge { get; set; }

        /// <summary>
        /// Признак установки значения после списка
        /// </summary>
        public bool IsEndListValue { get; set; }

        /// <summary>
        /// Название свойства
        /// </summary>
        public string Name { get; set; }
    }
}