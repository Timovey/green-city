using ExternalApi.Interface.Models.Auth;
using GreenCity.Http;
using GreenCityTests.Data;
using GreenCityTests.Data.Models;
using GreenCityTests.Tools;
using System.Threading.Tasks;
using Xunit;

namespace GreenCityTests.ApiTests.Auth
{
    [TestCaseOrderer("GreenCityTests.Tools.PriorityOrderer", "GreenCityTests")]
    public class AuthTests : BaseApiTests
    {
        //private void AssertAccountData(Account account, string roleName, CommonHttpResponse<ExtAuthResult> loginResult)
        //{
        //    Assert.NotNull(loginResult.Data);
        //    Assert.NotNull(loginResult.Data.Token);
        //    Assert.NotNull(loginResult.Data.UserId);
        //    Assert.Equal(roleName, loginResult.Data.Role);
        //    Assert.Equal(account.Name, loginResult.Data.UserName);
        //}

        //[Fact]
        //public async Task Try_Login_Admin()
        //{
        //    var account = AccountDataProvider.GetAdminAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    AssertAccountData(account, "admin", loginResult);
        //}

        //[Fact]
        //public async Task Try_Login_Tax()
        //{
        //    var account = AccountDataProvider.GetTaxAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    AssertAccountData(account, "tax", loginResult);
        //}

        //[Fact]
        //public async Task Try_Login_Logist()
        //{
        //    var account = AccountDataProvider.GetLogistAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    AssertAccountData(account, "logist", loginResult);
        //}

        //[Fact]
        //public async Task Try_Login_Dispatcher()
        //{
        //    var account = AccountDataProvider.GetDispatcherAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    AssertAccountData(account, "dispather", loginResult);
        //}

        //[Fact]
        //public async Task Try_Login_Ao()
        //{
        //    var account = AccountDataProvider.GetAOAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    AssertAccountData(account, "ao", loginResult);
        //}
    }
}
