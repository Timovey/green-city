using System.Collections.Generic;

namespace AuthService.Models.UserToOrgs
{
    public class UpdateUserToOrgsModel
    {
        public int UserToOrgsId { get; set; }

        /// <summary>
        /// ИД пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификаторы организаций
        /// </summary>
        public IList<int> OrgIds { get; set; }

        public int UpdatedUserId { get; set; }

        public string UpdatedUserName { get; set; }
    }
}
