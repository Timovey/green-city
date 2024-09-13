using ExternalApi.Interface.Models.Sd.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCityTests.Data.Sd
{
    public class ContainerGenerator
    {
        public static ExtCreateContainerRequest GenerateContainer(string userName, string userId, int? standId = null)
        {
            return new ExtCreateContainerRequest
            {
                Name = Settings.ContainerNamePrefix + Guid.NewGuid().ToString(),
                ContainerStandId = standId,
                UserId = Convert.ToInt32(userId),
                UserName = userName
            };
        }
    }
}
