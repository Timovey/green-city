namespace GreenCity.CommonUtils.CommonModels
{
    /// <summary>
    /// Базовый класс для сортировочных фильтров
    /// </summary>
    public class CommonSortingModel
    {
        public SortModel Id { get; set; } = new SortModel { SortType = SortType.Asc };
    }
}
