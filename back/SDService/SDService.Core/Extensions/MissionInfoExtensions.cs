using AutoMapper;
using GreenCity.CommonUtils.CommonExtensions;
using SDContracts.DataContracts.MissionInfo;
using SDContracts.ViewModels;
using SDContracts.ViewModels.MissionInfo;
using SDService.Database.Models.ViewModels.Container;
using System.Collections.Generic;
using System.Linq;

namespace SDService.Core.Extensions
{
    internal static class MissionInfoExtensions
    {
        internal static IEnumerable<MissionInfoViewModel> BuildMissionInfo(
            this IList<InfoForMissionModel> missionInfosFromStorage,
            IMapper mapper
        )
        {
            var result = new List<MissionInfoViewModel>();

            var infosGroupedByContainers = missionInfosFromStorage
                .GroupBy(x => x.ContainerId)
                .ToDictionary(x => x.Key, y => y.ToList());

            foreach (var el in infosGroupedByContainers)
            {
                var oneToOneEntities = el.Value.Select(x => new
                {
                    StandInfo = x.StandInfo,
                    ContainerTypeId = x.ContainerTypeId,
                    ContainerName = x.ContainerName,
                    ContainerTrashType = x.TrashType,
                    ContainerTypeName = x.ContainerTypeName,
                    Volume = x.Volume
                }).First();

                result.Add(new MissionInfoViewModel
                {
                    ContainerId = el.Key,
                    ContractClientNames = el.Value
                        .Where(x => !x.ContractClientName.IsNullOrEmpty())
                        .Select(x => x.ContractClientName)
                        .Distinct()
                        .ToList(),
                    StandInfo = mapper.Map<MissionStandInfoViewModel>(oneToOneEntities.StandInfo),
                    ContainerName = oneToOneEntities.ContainerName,
                    TrashType = oneToOneEntities.ContainerTrashType,
                    ContainerTypeId = oneToOneEntities.ContainerTypeId,
                    ContainerTypeName = oneToOneEntities.ContainerTypeName,
                    Volume = oneToOneEntities.Volume
                });
            }

            return result;
        }

        internal static FullDataModelForCreateFakeMissionInfoContract BuildDataForFakeMissionInfo(this CreateFakeDataForMissionInfoContract contract)
        {
            if (contract == null)
                return null;

            return new FullDataModelForCreateFakeMissionInfoContract
            {
                StandAddress = $"Точка на карте широта - {contract.Latitude} долгота - {contract.Longitude}",
                ContainerTypeName = $"Произвольный {contract.CustomVolume}",
                CustomVolume = contract.CustomVolume,
                Latitude = contract.Latitude,
                Longitude = contract.Longitude,
                CoordinatesComment = contract.CoordinatesComment,
                ContainerName = $"Точка на карте",
                ContractClientId = contract.ContractClientId,
                ContractClientName = $"КА для точки ({contract.Latitude};{contract.Longitude})",
                ContractId = contract.ContractId,
                ContractNumber = $"Договор для точки ({contract.Latitude};{contract.Longitude})",
                UserId = contract.UserId,
                UserName = contract.UserName
            };
        }
    }
}
