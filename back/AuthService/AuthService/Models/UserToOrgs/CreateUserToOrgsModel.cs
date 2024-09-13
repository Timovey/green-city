using System.Collections.Generic;

namespace AuthService.Models.UserToOrgs
{
    public class CreateUserToOrgsModel
    {
        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификаторы организаций
        /// </summary>
        public IList<int> OrgIds { get; set; }

        public int CreatedUserId { get; set; }

        public string CreatedUserName { get; set; }
    }
}
