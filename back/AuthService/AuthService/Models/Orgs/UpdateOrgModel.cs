using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Orgs
{
    public class UpdateOrgModel
    {
        [Required]
        public IList<int> OrganizationIds { get; set; }

        /// <summary> Название </summary>
        [Required, MaxLength(255)]
        public string Name { get; set; }

        /// <summary> Адрес </summary>
        [Required, MaxLength(512)]
        public string Address { get; set; }

        /// <summary> Телефон </summary>
        [Required, MaxLength(50)]
        public string Phone { get; set; }

        /// <summary> ОГРН </summary>
        [Required, MaxLength(50)]
        public string Ogrn { get; set; }
    }
}
