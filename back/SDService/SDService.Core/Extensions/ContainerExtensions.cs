using SDService.Database.Models.DbModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SDService.Core.Extensions
{
    public static class ContainerExtensions
    {
        private const char _separator = ',';

        public static string GetContainerInfoForRoute(this IList<(Container Container, ContainerType ContainerType)> containers)
        {
            var containersGroupedByContainerTypeId = containers
                .GroupBy(x => x.ContainerType.Id)
                .ToDictionary(x => x.Key, value => (
                    ContainerType: value.Select(x => x.ContainerType).FirstOrDefault(),
                    Containers: value.Select(x => x.Container).ToList()
                 ));

            var containerStringList = new List<string>();
            foreach(var key in containersGroupedByContainerTypeId.Keys)
            {
                var containerInfo = containersGroupedByContainerTypeId[key];
                var containerTypeVolume = containerInfo.ContainerType.Volume;
                var containersCount = containerInfo.Containers.Count;

                containerStringList.Add($"{containerTypeVolume.ToString(CultureInfo.InvariantCulture)}*{containersCount}");
            }

            return string.Join(_separator, containerStringList);
        }
    }
}
