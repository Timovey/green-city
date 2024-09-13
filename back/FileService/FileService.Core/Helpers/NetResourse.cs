using System.Runtime.InteropServices;

namespace FileService.Core.Helpers
{
    /// <summary>
    /// Маршилирование структуры. Здесь указывается, что важны не названия, 
    /// а порядок структуры для ее передачи в неуправляемый метод (LayoutKind.Sequential)
    /// Аналог структуры _NETRESOURCE
    /// Эта структура, которая содержит данные о сетевых ресурсах. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope;
        public ResourceType ResourceType;
        public ResourceDisplayType DisplayType;
        /// <summary>
        /// Задает битовую маску, которая дает использование ресурсов. 
        /// Этот член определен, 
        /// только если dwScope имеет значение RESOURCE_GLOBALNET
        /// </summary>
        public int Usage;
        /// <summary>
        /// Длинный указатель на локальное имя сетевого ресурса, 
        /// если элемент dwScope имеет значение RESOURCE_CONNECTED или RESOURCE_REMEMBERED
        /// </summary>
        public string LocalName;
        /// <summary>
        /// Длинный указатель на имя удаленной сети, если запись является сетевым ресурсом.
        /// </summary>
        public string RemoteName;
        /// <summary>
        /// Длинный указатель на предоставленный провайдером комментарий
        /// </summary>
        public string Comment;
        /// <summary>
        /// Длинный указатель на имя провайдера, которому принадлежит этот ресурс
        /// </summary>
        public string Provider;
    }
}
