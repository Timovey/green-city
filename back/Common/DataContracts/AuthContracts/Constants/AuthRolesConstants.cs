namespace AuthContracts.Constants
{
    public class AuthRolesConstants
    {
        /// <summary> Роль администратора </summary>
        public const string ADMIN = "admin";

        /// <summary> Роль таксировщика </summary>
        public const string TAX = "tax";

        /// <summary> Роль диспетчера </summary>
        public const string DISPATCHER = "dispatcher";

        /// <summary> Роль логиста </summary>
        public const string LOGIST = "logist";

        /// <summary> Роль менеджера АО </summary>
        public const string AO = "ao";

        /// <summary> Роль менеджера АО с расширенными правами </summary>
        public const string ABSOLUTE_APP_ACTION_ROLE = "absoluteAppAction";

        public static string GetStringRoles(params string[] roles) 
        {
            return string.Join(",", roles);
        }
    }
}
