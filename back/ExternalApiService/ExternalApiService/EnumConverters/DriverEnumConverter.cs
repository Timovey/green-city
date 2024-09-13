using ExternalApi.Interface.Models.Transport.Driver;
using ExternalApi.Interface.Models.Transport.DriverVacation;
using System.Collections.Generic;

namespace ExternalApiService.EnumConverters
{
    internal static class DriverEnumConverter
    {
		internal static string ToExtDrivingSkillsTypeName(this ExtDrivingSkillsType drivingSkillsType)
		   => drivingSkillsType switch
		   {
			   ExtDrivingSkillsType.Unknown => "Неизвестно",
			   ExtDrivingSkillsType.BunkerTruck => "Бункеровоз",
			   ExtDrivingSkillsType.DumpTruck => "Самосвал",
			   ExtDrivingSkillsType.GarbageTruck => "Мусоровоз",
			   _ => "Error: Неизвестно"
		   };

        internal static IEnumerable<string> ToDrivingSkillsName(this IList<ExtDrivingSkillsType> skillTypes)
        {
            foreach (var skillType in skillTypes)
            {
                yield return skillType.ToExtDrivingSkillsTypeName();
            }

        }
		internal static string ToExtDriverVacationTypeName(this ExtDriverVacationType vacationType)
		   => vacationType switch
		   {
			   ExtDriverVacationType.Unknown => "Неизвестно",
			   ExtDriverVacationType.SickLeave => "Больничный",
			   ExtDriverVacationType.Vacation => "Отпуск",
			   _ => "Error: Неизвестно"
		   };
	}
}
