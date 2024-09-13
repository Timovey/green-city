using MissionService.Contracts.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MissionService.Core.Extensions
{
    public static class AppMissionStatusesExtensions
    {
        public static AppComputedStatus GetAppComputedStatus(this IList<MissionStatus> missionStatuses)
        {
            if (missionStatuses == default || missionStatuses.Count == 0)
                return AppComputedStatus.Unknown;

            if (missionStatuses.Any(x => x == MissionStatus.Unknown))
            {
                return AppComputedStatus.Unknown;
            }

            //!_! Если есть хоть одна проваленая, то считаем заявку проваленой
            if (missionStatuses.Where(x => x == MissionStatus.Failed).FirstOrDefault() != default)
            {
                return AppComputedStatus.Failed;
            }

            //!_! Если нет проваленых и неизвестных и есть хоть одна выполненая, то считаем частично выполненой
            if (missionStatuses.Where(x => x == MissionStatus.Done).FirstOrDefault() != default)
            {
                //!_! Если все выполнены, то заявка выполнена
                if (missionStatuses.All(x => x == MissionStatus.Done))
                {
                    return AppComputedStatus.Done;
                }

                //!_! Если не все, то частично выполнена
                return AppComputedStatus.PartiallyDone;
            }

            //!_! Если хоть одна в процессе, то заявку тоже считаем в процессе
            if (missionStatuses.Where(x => x == MissionStatus.InProgress).FirstOrDefault() != default)
            {
                return AppComputedStatus.InProgress;
            }

            //!_! Если дошли сюда, то значит все статусы == New
            return AppComputedStatus.New;
        }
    }
}
