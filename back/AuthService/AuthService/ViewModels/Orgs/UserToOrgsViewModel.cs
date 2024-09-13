using System.Collections.Generic;

namespace AuthService.ViewModels.Orgs
{
    public class UserToOrgsViewModel
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
    }
}
