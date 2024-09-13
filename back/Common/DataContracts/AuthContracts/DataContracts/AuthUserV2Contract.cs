using GreenCity.Http;
using GreenCity.Http.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace AuthContracts.DataContracts
{
    /// <summary>
    /// Контракт на авторизацию
    /// </summary>
    public class AuthUserV2Contract : CommonHttpContract
    {
        /// <summary>
        /// Логин
        /// </summary>
        [GCRequired]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [GCRequired]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
