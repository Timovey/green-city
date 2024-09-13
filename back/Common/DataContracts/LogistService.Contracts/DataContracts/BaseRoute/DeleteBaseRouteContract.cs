using System.Collections.Generic;
using GreenCity.Http;

namespace LogistService.Contracts.DataContracts.BaseRoute;

/// <summary> Контракт на удаление базовых маршрутов </summary>
public class DeleteBaseRouteContract : CommonHttpContract
{
    /// <summary> ИД базовых маршрутов </summary>
    public IList<int> Ids { get; set; }
}