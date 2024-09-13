namespace ExternalApi.Interface.Models.Sd.Contract
{
    public class ExtContractContainerViewModel
    {
        /// <summary> ИД контейнера </summary>
        public int ContainerId { get; set; }

        /// <summary> Наименование контейнера </summary>
        public string Name { get; set; }

        /// <summary> Тип тары </summary>
        public int ContainerTypeId { get; set; }

        public ExtContainer2ScheduleViewModel Container2Schedule { get; set; }

        /// <summary> Находится ли контейнер в договоре </summary>
        public bool IsInContract { get; set; }
    }
}
