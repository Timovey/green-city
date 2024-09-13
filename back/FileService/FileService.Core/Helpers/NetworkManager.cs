using FileService.Core.Models;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FileService.Core.Helpers
{
    /// <summary>
    /// Класс для подключения к удаленному вирутальному диску
    /// </summary>
    public class NetworkManager : IDisposable
    {
        private readonly string _networkName;
        private readonly VirtualDriveSettings _settings;

        public NetworkManager(VirtualDriveSettings settings)
        {
            _settings = settings;
            _networkName = _settings.IP;

            //Создание сетевой структуры
            NetResource netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Any,
                DisplayType = ResourceDisplayType.Directory,
                RemoteName = _networkName
            };

            int result = WNetAddConnection2(netResource, _settings.Password, _settings.Username, 0);

            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }

        ~NetworkManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            WNetCancelConnection2(_networkName, 0, true);
        }

        /// <summary>
        /// устанавливает соединение с сетевым ресурсом и может перенаправить локальное устройство на сетевой ресурс
        /// </summary>
        /// <param name="netResource">Тип сетевого ресурса для подключения</param>
        /// <param name="password">пароль пользователя удаленного рабочего стола</param>
        /// <param name="username">имя пользователя удаленного рабочего стола</param>
        /// <param name="flags">Набор параметров подключения. Возможные значения параметров соединения
        /// определены в заголовочном файле Winnetwk.h</param>
        /// <returns></returns>
        //mpr.dll - Библиотека маршрутизации для нескольких служб доступа 
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);

        /// <summary>
        /// закрывает соединение с сетевым ресурсом
        /// </summary>
        /// <param name="name">Указатель на строку, 
        /// которая указывает имя либо перенаправленного локального устройства,
        /// либо удаленного сетевого ресурса, от которого необходимо отключиться.</param>
        /// <param name="flags">Тип соединения</param>
        /// <param name="force">Указывает, должно ли происходить отключение, 
        /// если в соединении есть открытые файлы или задания</param>
        /// <returns></returns>
        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);
    }

}
