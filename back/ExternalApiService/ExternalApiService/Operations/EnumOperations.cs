using ExternalApi.Interface.Models.Enums;
using ExternalApiService.Operations.Interface;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalApiService.EnumConverters;
using System.Reflection;
using System.IO;
using System.Linq;

namespace ExternalApiService.Operations
{
    public class EnumOperations : IEnumOperations
    {
        List<Type> transportEnums;
        List<Type> aoEnums;
        List<Type> missionEnums;
        List<Type> logistEnums;

        public EnumOperations()
        {
            //с помощью рефлексии находим в сборке все енамы
            //в их полном названии фигурирует название модуля (Transport,Sd)
            transportEnums = new List<Type>();
            aoEnums = new List<Type>();

            //dll находится в папке /bin/[Debug || Release]
            Assembly asm = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExternalApi.Interface.dll"));
            Type[] allTypes = asm.GetTypes();

            transportEnums = allTypes.Where(el => el.FullName.Contains("Models.Transport") && el.IsEnum).ToList();
            aoEnums = allTypes.Where(el => el.FullName.Contains("Models.Sd") && el.IsEnum).ToList();
            missionEnums = allTypes.Where(el => el.FullName.Contains("Models.Mission") && el.IsEnum).ToList();
            logistEnums = allTypes.Where(el => el.FullName.Contains("Models.Logist") && el.IsEnum).ToList();

        }
        private List<Type> GetModuleEnums(ModuleType moduleType)
        {
            switch(moduleType)
            {
                case ModuleType.Transport:
                    return transportEnums;
                case ModuleType.SD:
                    return aoEnums;
                case ModuleType.Logist:
                    return logistEnums;
                case ModuleType.Mission:
                    return missionEnums;
                default:
                    return transportEnums;
            }
        }
        public async Task<CommonHttpResponse<EnumViewModel>> GetEnums(ModuleType moduleType)
        {
            EnumViewModel result = new EnumViewModel()
            {
                EnumValues = new Dictionary<string, IDictionary<int, string>>()
            };

            List<string> enumNames = new List<string>();
            //собираем все типы перечислений в зависимости, какой модуль нужен
            List<Type> allEnums = GetModuleEnums(moduleType);

            //проходимся по типам
            foreach (Type en in allEnums)
            {
                //на каждый тип заводим словарь (значение - название)
                Dictionary<int, string> enDist = new Dictionary<int, string>();
                //получаем все значения перечисления
                foreach(Enum type in Enum.GetValues(en))
                {
                    //с помощью конвертера и атрибута получаем имя
                    string name = type.DisplayName();
                    //добавляем в словарь
                    enDist.Add(Convert.ToInt32(type), name);
                }
                //с помощью конвертера преобразуем имя
                //(убираем ext и type из названия)
                string enumName = en.GetName();
                result.EnumValues.Add(enumName, enDist);
                enumNames.Add(enumName);
            }
            result.Names = enumNames.ToArray();
            return CommonHttpHelper.BuildSuccessResponse(result,
                    System.Net.HttpStatusCode.OK);
        }

    }
}
