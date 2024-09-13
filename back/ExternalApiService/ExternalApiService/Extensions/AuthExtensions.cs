using AuthContracts.Constants;
using ExternalApi.Interface.Models.Mission.App;
using System.Security.Claims;

namespace ExternalApiService.Extensions
{
    internal static class AuthExtensions
    {
        public static ExtAppActionsPermission GetAppActionsPermission(this ClaimsPrincipal user)
        {
            if (user == default)
                return ExtAppActionsPermission.Unknown;

            if (user.IsInOneOfRoles(AuthRolesConstants.ABSOLUTE_APP_ACTION_ROLE, AuthRolesConstants.ADMIN))
                return ExtAppActionsPermission.Absolute;

            if (user.IsInOneOfRoles(AuthRolesConstants.AO, AuthRolesConstants.LOGIST))
                return ExtAppActionsPermission.Default;

            return ExtAppActionsPermission.Unknown;
        }

        /// <summary>
        /// Проверяет наличие у пользователя хотябы одной из переданной ролей
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsInOneOfRoles(this ClaimsPrincipal user, params string[] roles)
        {
            if (user == default || roles == default || roles.Length == 0)
                return false;

            foreach (var role in roles)
            {
                if (user.IsInRole(role))
                    return true;
            }

            return false;
        }
    }
}
