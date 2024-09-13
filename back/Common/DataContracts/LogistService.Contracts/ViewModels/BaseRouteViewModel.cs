using System;
using System.Collections.Generic;

namespace LogistService.Contracts.ViewModels
{
    public class BaseRouteViewModel
    {
        /// <summary> ИД БМ </summary>
        public int Id { get; set; }

        /// <summary> Название БМ </summary>
        public string Name { get; set; }

        
        /// <summary> Массив контейнерных площадок </summary>
        /// (используется System.Tuple, так как другой кортеж C# обнуляет значения при передаче в external.
        /// В текущем кортеже нельзя задать названия переменных)
        public IList<Tuple<int, int>> Stands { get; set; }
    }
}