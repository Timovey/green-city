using GreenCity.Http;

namespace FileService.Contracts.DataContracts.File
{
    public class DownloadFileByIdContract : CommonHttpContract
    {
        /// <summary> ID файла</summary>
        public int Id { get; set; }
    }
}
