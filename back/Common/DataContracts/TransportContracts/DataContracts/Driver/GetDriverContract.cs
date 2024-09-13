using GreenCity.Http;
using System.Collections.Generic;

namespace TransportContracts.DataContracts.Driver
{
    /// <summary> Получение водителей </summary>
    public class GetDriverContract : CommonHttpContract
    {
        /// <summary> ИД водителей </summary>
        public IList<int> DriverIds { get; set; }

        /// <summary>
        /// false - если не найдено то ошибка, true - если не найдено то пустой список
        /// </summary>
        public bool ReturnEmptyResultIfNotFound { get; set; }
    }
}
