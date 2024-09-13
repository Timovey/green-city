using TransportContracts.DataContracts.Enums;

namespace TransportService.Core.Helpers
{
    internal static class DrivingSkillsHelper
    {
        internal static bool IsCanDrive(IList<DrivingSkillsType> originalDriverDrivingSkills, IList<DrivingSkillsType> replaceDriverDrivingSkills)
        {
            foreach(var originalSkill in originalDriverDrivingSkills)
            {
                if(!replaceDriverDrivingSkills.Contains(originalSkill))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
