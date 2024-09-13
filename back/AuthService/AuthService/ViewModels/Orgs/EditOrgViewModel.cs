using GreenCity.Http.Attributes.Validation;

namespace AuthService.ViewModels.Orgs
{
    public class EditOrgViewModel
    {
        public int OrganizationId { get; set; }

        /// <summary> Название </summary>
        [GCRequired, GCMaxLength(255)]
        public string Name { get; set; }

        /// <summary> Адрес </summary>
        [GCRequired, GCMaxLength(512)]
        public string Address { get; set; }

        /// <summary> Телефон </summary>
        [GCRequired, GCMaxLength(50)]
        public string Phone { get; set; }

        /// <summary> ОГРН </summary>
        [GCRequired, GCMaxLength(50)]
        public string Ogrn { get; set; }
    }
}
