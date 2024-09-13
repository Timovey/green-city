using Microsoft.AspNetCore.Authorization;

namespace AuthContracts.Attributres
{
    /// <summary>
    /// Атрибут авторизации Microsoft.AspNetCore.Authorization с прописыванием ролей 
    /// </summary>
    public class AuthRolesAttribute : AuthorizeAttribute
    {
        public AuthRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
