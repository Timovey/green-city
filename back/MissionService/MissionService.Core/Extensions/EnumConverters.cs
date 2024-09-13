using SDContracts.Enums;

namespace MissionService.Core.Extensions
{
    internal static class EnumConverters
    {
		internal static string ToStandTypeText(this StandType standType)
		   => standType switch
		   {
			   StandType.Unknown => "Неизвестно",
			   StandType.HouseStock => "Жилой фонд",
			   StandType.Default => "Обычный",
			   _ => "Error: Неизвестно"
		   };
		
		internal static string ToTrashTypeText(this TrashType trashType)
			=> trashType switch
			{
				TrashType.TKO => "ТКО",
				TrashType.KGO => "КГО",
				TrashType.Class4 => "4 класс",
				TrashType.Class5 => "5 класс",
				TrashType.Construct => "Строительный",
				TrashType.Unknown => "Неизвестно",
				_ => "Error: Неизвестно"
			};
	}
}
