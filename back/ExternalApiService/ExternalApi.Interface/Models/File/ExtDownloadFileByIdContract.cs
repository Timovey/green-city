using GreenCity.Http;

namespace ExternalApi.Interface.Models.File
{
    public class ExtDownloadFileByIdContract : CommonHttpContract
    {
        /// <summary> ID файла</summary>
        public int Id { get; set; }
    }
}
