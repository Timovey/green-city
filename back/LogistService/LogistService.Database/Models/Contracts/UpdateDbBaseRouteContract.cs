using System.Collections.Generic;

namespace LogistService.Database.Models.Contracts
{
    /// <summary> Контракт для БД на обновление информации по базовому маршруту </summary>
    public class UpdateDbBaseRouteContract
    {
        /// <summary> ИД обновляемого БМ </summary>
        public int Id { get; set; }

        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        /// <summary> Массив ИД контейнерных площадок с приоритетами </summary>
        public IList<(int priority, int standId)> StandIdsWithPriorities { get; set; }

        /// <summary> ИД пользователя создателя </summary>
        public int CreatedUserId { get; set; }

        /// <summary> Имя пользователя создателя </summary>
        public string CreatedUserName { get; set; }
    }
}
