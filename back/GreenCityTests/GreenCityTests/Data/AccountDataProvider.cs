using GreenCityTests.Data.Models;

namespace GreenCityTests.Data
{
    internal class AccountDataProvider
    {
        public static Account GetLogistAccount()
        {
            return new Account
            {
                Email = "barbosik@gmail.com",
                Password = "Aa1234567890228f!",
                Name = "Barbosik_logist"
            };
        }

        public static Account GetTaxAccount()
        {
            return new Account
            {
                Email = "antony@gmail.com",
                Password = "Aa1234567890228f!",
                Name = "Antony_tax"
            };
        }

        public static Account GetDispatcherAccount()
        {
            return new Account
            {
                Email = "chelik@gmail.com",
                Password = "Aa1234567890228f!",
                Name = "Chelik_dispatcher"
            };
        }

        public static Account GetAOAccount()
        {
            return new Account
            {
                Email = "keksik@gmail.com",
                Password = "Aa1234567890228f!",
                Name = "Keksik_ao"
            };
        }

        public static Account GetAdminAccount()
        {
            return new Account
            {
                Email = "test_admin@gmail.com",
                Password = "Aa1234567890228f!",
                Name = "test_admin_admin"
            };
        }
    }
}
