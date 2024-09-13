using GreenCity.Http;
using System.ComponentModel.DataAnnotations;

namespace AuthContracts.DataContracts
{
    /// <summary>
    /// Контракт на авторизацию
    /// </summary>
    public class AuthUserContract : CommonHttpContract
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
