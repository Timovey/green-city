namespace AuthContracts.DataContracts
{
    /// <summary>
    /// Результат авторизации пользователя
    /// </summary>
    public class AuthUserResult
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; }

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
