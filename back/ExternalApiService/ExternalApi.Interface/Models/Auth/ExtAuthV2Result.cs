using System.Collections.Generic;

namespace ExternalApi.Interface.Models.Auth
{
    public class ExtAuthV2Result
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }
    }
}
