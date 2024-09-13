using ExternalApi.Interface.Models.Auth;
using ExternalApi.Interface.Models.Sd.Container;
using GreenCityTests.Data;
using GreenCityTests.Data.Sd;
using GreenCityTests.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GreenCityTests.ApiTests.Sd
{
    //!_! Почисти тестовую базу перез запуском тестов!!!
    [TestCaseOrderer("GreenCityTests.Tools.PriorityOrderer", "GreenCityTests")]
    public class ContainerTests : BaseApiTests
    {
        //private readonly string _myUniqPrefixContainer1 = "MY_UNIQ_PREFIX";
        //private readonly string _containerTempDataFileName = "containers_temp_data";
        //private readonly string _containerTokenSavedFileName = "containers_token_data";

        //private string _token = string.Empty;
        //private string _name = string.Empty;
        //private string _userId = string.Empty;

        //private Dictionary<int, ExtContainerViewModel> _createdContainers;

        //private async Task<string> GetToken()
        //{
        //    if (string.IsNullOrEmpty(_token))
        //    {
        //        _token = "";
        //    }
        //    return _token;
        //}

        //private async Task RestoreDict()
        //{
        //    _createdContainers = await TempFileDataSaver.ReadJsonFromFileAsync<Dictionary<int, ExtContainerViewModel>>(_containerTempDataFileName);
        //}

        //private async Task SaveDict()
        //{
        //    await TempFileDataSaver.WriteJsonToFileAsync(_containerTempDataFileName, _createdContainers);
        //}

        //[Fact, TestPriority(1)]
        //public async Task ContainerTests__PrepareData__GetToken()
        //{
        //    var account = AccountDataProvider.GetAOAccount();
        //    var loginRequest = new ExtAuthRequest { Login = account.Email, Password = account.Password };

        //    var loginResult = await ExternalApiClient.Login(loginRequest);
        //    AssertSuccessResult(loginResult);

        //    _token = loginResult.Data.Token;
        //    _name = loginResult.Data.UserName;
        //    _userId = loginResult.Data.UserId.ToString();
        //}

        //[Fact, TestPriority(2)]
        //public async Task ContainersTests__PrepareData__CreateContainers()
        //{
        //    var container1Req = ContainerGenerator.GenerateContainer(_name, _userId);
        //    var container2Req = ContainerGenerator.GenerateContainer(_name, _userId);
        //    var container3Req = ContainerGenerator.GenerateContainer(_name, _userId);

        //    if (_createdContainers == null)
        //    {
        //        await RestoreDict();
        //    }

        //    await CreateContainer(container1Req);
        //    await CreateContainer(container2Req);
        //    await CreateContainer(container3Req);

        //    await SaveDict();
        //}

        //private async Task CreateContainer(ExtCreateContainerRequest request)
        //{
        //    var createContainerResult = await ExternalApiClient.CreateContainer(request, _token);
        //    AssertSuccessResult(createContainerResult);
        //    Assert.True(createContainerResult.Data > 0);
        //    _createdContainers.Add(createContainerResult.Data, null);
        //}

        //[Fact, TestPriority(3)]
        //public async Task ContainersTests__PrepareData__CheckSavedContainers__Then__SaveToDictionary()
        //{
        //    var getReq = new ExtGetContainerRequest
        //    {
        //        ContainerIds = _createdContainers.Select(x => x.Key).ToArray(),
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var containers = await ExternalApiClient.GetContainer(getReq, _token);
        //    AssertSuccessResult(containers);

        //    Assert.True(containers.Data != null && containers.Data.Length == 3);

        //    //!_! Сохраняем контейнеры для дальшейших тестов
        //    foreach (var el in containers.Data)
        //    {
        //        _createdContainers[el.ContainerId] = el;
        //    }
        //}

        //[Fact, TestPriority(4)]
        //public async Task ContainersTests__BadFilter__ThenNoOneContainerReturns()
        //{
        //    var filterReq = new ExtContainerFilterRequest
        //    {
        //        Name = "BAD_FILTER_THIS_STRING_IS_BAD",
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var containers = await ExternalApiClient.GetFilteredContainer(filterReq, _token);
        //    AssertSuccessResult(containers);

        //    Assert.True(containers.Data != null && containers.Data.Length == 0);
        //}

        //[Fact, TestPriority(4)]
        //public async Task ContainersTests__GoodFilter__ThenAllContainerReturns()
        //{
        //    var filterReq = new ExtContainerFilterRequest
        //    {
        //        Name = Settings.ContainerNamePrefix,
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var containers = await ExternalApiClient.GetFilteredContainer(filterReq, _token);
        //    AssertSuccessResult(containers);

        //    Assert.True(containers.Data != null && containers.Data.Length == _createdContainers.Count);
        //}

        //[Fact, TestPriority(5)]
        //public async Task ContainersTests__UpdateOneContainer__ThenApplyFilter_ThenOnlyOneContainerReturns()
        //{
        //    var updateContainer = _createdContainers.First();
        //    var newName = _myUniqPrefixContainer1 + Guid.NewGuid().ToString();

        //    var updateReq = new ExtUpdateContainerRequest
        //    {
        //        ContainerIds = new List<int> { updateContainer.Value.ContainerId },
        //        Name = newName,
        //        ContainerStandId = updateContainer.Value.ContainerStandId,
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var updateResult = await ExternalApiClient.UpdateContainer(updateReq, _token);
        //    AssertSuccessResult(updateResult);

        //    var filterReq = new ExtContainerFilterRequest
        //    {
        //        Name = newName,
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var containers = await ExternalApiClient.GetFilteredContainer(filterReq, _token);
        //    AssertSuccessResult(containers);

        //    Assert.True(containers.Data != null && containers.Data.Length == 1);
        //    var updatedContainer = containers.Data.Single();

        //    Assert.Equal(updateContainer.Value.ContainerId, updatedContainer.ContainerId);
        //    Assert.Equal(newName, updatedContainer.Name);
        //}

        //[Fact, TestPriority(6)]
        //public async Task ContainersTests__DeleteOneContainer__ThenGetByFilter__ThenLessElementsReturns()
        //{
        //    var containerToDelete = _createdContainers.FirstOrDefault(x => x.Value.Name.Contains(_myUniqPrefixContainer1)).Value;

        //    var deleteRequest = new ExtDeleteContainerRequest
        //    {
        //        ContainerIds = new List<int> { containerToDelete.ContainerId },
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var deleteResult = await ExternalApiClient.DeleteContainer(deleteRequest, _token);
        //    AssertSuccessResult(deleteResult);

        //    var filterReq = new ExtContainerFilterRequest
        //    {
        //        IsFilter = false,
        //        UserId = Convert.ToInt32(_userId),
        //        UserName = _name
        //    };

        //    var allContainers = await ExternalApiClient.GetFilteredContainer(filterReq, _token);
        //    AssertSuccessResult(allContainers);
        //    Assert.True(allContainers.Data != null && allContainers.Data.Length > 0);

        //    var deletedElem = allContainers.Data.FirstOrDefault(x => x.ContainerId == containerToDelete.ContainerId);
        //    Assert.Null(deletedElem);
        //}

        //[Fact, TestPriority(2)]
        //public async Task ContainersTests__CheckModelStateAttributes__CreateNotValidContainers()
        //{
        //    var badContainerReq = ContainerGenerator.GenerateContainer(_name, _userId);

        //    badContainerReq.Name = BadDataGenerator.GetVeryBigString(500);

        //    var createResult = await ExternalApiClient.CreateContainer(badContainerReq, _token);
        //    AssertErrorResult(createResult);

        //    //!_! Одна ошибка - слишком длинная строка
        //    Assert.True(createResult.Errors.Length == 1);
        //}
    }
}
