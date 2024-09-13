using GreenCity.Http;

namespace LogistService.Contracts.DataContracts.Route
{
    /// <summary> Контракт на удаление рейса </summary>
    public class DeleteRouteContract : CommonHttpContract
    {
        /// <summary> ИД рейса </summary>
        public int Id { get; set; }
    }
}