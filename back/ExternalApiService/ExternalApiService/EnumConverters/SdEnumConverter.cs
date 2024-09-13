using ExternalApi.Interface.Models.Sd.Container;
using ExternalApi.Interface.Models.Sd.Stand;

namespace ExternalApiService.EnumConverters
{
    internal static class SdEnumConverter
    {
		internal static string ToExtTrashTypeName(this ExtTrashType trashType)
			=> trashType switch
			{
				ExtTrashType.Unknown => "Неизвестно",
				ExtTrashType.TKO => "ТКО",
				ExtTrashType.KGO => "КГО",
				ExtTrashType.Class4 => "4 КЛАСС",
				ExtTrashType.Class5 => "5 КЛАСС",
				ExtTrashType.Construct => "СТРОИТЕЛЬНЫЙ",
				_ => "Error: Неизвестно"
			};

		internal static string ToExtStandTypeName(this ExtStandType containetType)
		   	=> containetType switch
			{
				ExtStandType.Unknown => "Неизвестно",
				ExtStandType.Default => "ОБЩИЙ ТИП КП",
				ExtStandType.HouseStock => "ЖИЛОЙ ФОНД",
				_ => "Error: Неизвестно"
			};
	}
}
