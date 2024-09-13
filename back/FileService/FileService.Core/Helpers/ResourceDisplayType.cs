namespace FileService.Core.Helpers
{
    /// <summary>
    /// Указывает, как сетевой объект должен отображаться в пользовательском интерфейсе просмотра сети
    /// </summary>
    public enum ResourceDisplayType
    {
        /// <summary>
        /// Метод, используемый для отображения объекта, не имеет значения
        /// </summary>
        Generic = 0x0,
        /// <summary>
        /// Объект должен отображаться как домен
        /// </summary>
        Domain = 0x01,
        /// <summary>
        /// Объект должен отображаться как сервер
        /// </summary>
        Server = 0x02,
        /// <summary>
        /// Объект должен отображаться как общий ресурс
        /// </summary>
        Share = 0x03,
        File = 0x04,
        Group = 0x05,
        Network = 0x06,
        Root = 0x07,
        Shareadmin = 0x08,
        Directory = 0x09,
        Tree = 0x0a,
        Ndscontainer = 0x0b
    }
}
