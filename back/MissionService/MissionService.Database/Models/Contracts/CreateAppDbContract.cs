using MissionService.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace MissionService.Database.Models.Contracts
{
    /// <summary>
    /// DB контракт на создание заявки с линком на миссии
    /// </summary>
    public class CreateAppDbContract
    {
        /// <summary> Дата заявки </summary>
        public DateTime Date { get; set; }

        /// <summary> ИД контрагента </summary>
        public int? ContractClientId { get; set; }

        /// <summary> ИД договора </summary>
        public int ContractId { get; set; }

        /// <summary> Тип заявки </summary>
        public AppType Type { get; set; }

        /// <summary> Тип создателя заявки </summary>
        public CreatorType CreatorType { get; set; }

        /// <summary> Комментарий </summary>
        public string Comment { get; set; }

        /// <summary> ИД миссий </summary>
        public IList<int> MissionIds { get; set; }

        /// <summary> ИД пользователя создателя </summary>
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создавшего запись </summary>
        public string CreatedUserName { get; set; }
    }
}
