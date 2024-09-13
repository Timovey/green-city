using DataGenerationService.Core.BusinessLogic;
using DataGenerationService.Core.MappingProfile;
using DataGenerationService.Main;
using GreenCity.Db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDService.Database;
using SDService.Database.Implements;
using SDService.Database.Interfaces;

IConfigurationRoot configurationRoot = null;

using var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configuration =>
    {
        configuration.Sources.Clear();

        configuration
            .SetBasePath(Directory.GetCurrentDirectory());

        configurationRoot = configuration.Build();
    })
    .ConfigureServices(services =>
    {
        var connectionString =
            "Data Source=localhost\\SQLEXPRESS,1433;Initial Catalog=SdDb;User ID=sa;Password=GreenCity_Dev_Passw0rd_1";
        services.AddDbContext<SdDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<GeneratingContainerLogic>();
        services.AddScoped<GeneratingStandLogic>();
        services.AddScoped<GeneratingContainerTypeLogic>();
        services.AddScoped<GeneratingContractLogic>();
        services.AddScoped<GeneratingContractClientLogic>();
        services.AddScoped<GeneratingContract2StandLogic>();

        services.AddScoped<IContainerStorage, ContainerStorage>();
        services.AddScoped<IStandStorage, StandStorage>();
        services.AddScoped<IContractStorage, ContractStorage>();
        services.AddScoped<IContractClientStorage, ContractClientStorage>();
        services.AddScoped<FillHistoryService>();

        services.AddTransient<App>();
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
    })
    .Build();

await builder.Services.GetService<App>().RunAsync(args);