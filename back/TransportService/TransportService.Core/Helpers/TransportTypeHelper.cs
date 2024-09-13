using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.Helpers
{
    internal static class TransportTypeHelper
    {
        internal static TransportType ToTransportType(this DrivingSkillsType drivingSkillsType)
           => drivingSkillsType switch
           {
               DrivingSkillsType.Unknown => TransportType.Unknown,
               DrivingSkillsType.BunkerTruck => TransportType.BunkerTruck,
               DrivingSkillsType.DumpTruck => TransportType.DumpTruck,
               DrivingSkillsType.GarbageTruck => TransportType.GarbageTruck,
               _ => TransportType.Unknown
           };

        internal static bool IsCompatible(this IList<DrivingSkillsType> drivingSkillsType, TransportType transportType)
        {
            foreach (var skillType in drivingSkillsType)
            {
                if(CompareTypesDriverTransport(skillType ,transportType)) return true; 
            }
            return false;

        }

        private static bool CompareTypesDriverTransport(DrivingSkillsType drivingSkillsType, TransportType transportType)
        {
            if(drivingSkillsType == DrivingSkillsType.GarbageTruck && transportType == TransportType.GarbageTruck)
                return true;
            else if(drivingSkillsType == DrivingSkillsType.DumpTruck && transportType == TransportType.DumpTruck)
                return true;
            else if (drivingSkillsType == DrivingSkillsType.BunkerTruck && transportType == TransportType.BunkerTruck)
                return true;
            return false;
        }
    }
}
