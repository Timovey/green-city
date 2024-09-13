using AuthContracts.Interfaces;
using GreenCity.Db.Services;
using GreenCity.Logging.DbLogging;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using ScheduleService.Contracts.Interfaces;
using System;
using System.Text.Json;
using TransportService.Core.BusinessLogic;
using TransportService.Core.MappingProfiles;
using TransportService.Database;
using TransportService.Database.Implements;
using TransportService.Database.Interfaces;
using TransportService.Database.MappingProfiles;
using TransportService.Main;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var connectionString = GlobalSettings.IsUnitTesting ? "ConnectionStringUT" : "ConnectionString";

builder.Services.AddDbContext<TransportDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerDb:{connectionString}"]));

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration[$"MsSqlServerDb:{connectionString}"], new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
builder.Services.AddHangfireServer();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ContractsMappingProfile>());

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<FillHistoryService>();

builder.Services.AddScoped<ITransportStorage, TransportStorage>();
builder.Services.AddScoped<TransportLogic>();
builder.Services.AddScoped<ITtansportSheduleStorage, TransportSheduleStorage>();
builder.Services.AddScoped<TransportSheduleLogic>();
builder.Services.AddScoped<IDriver2TransportStorage, Driver2TransportStorage>();
builder.Services.AddScoped<Driver2TransportLogic>();
builder.Services.AddScoped<IDriverStorage, DriverStorage>();
builder.Services.AddScoped<DriverLogic>();
builder.Services.AddScoped<IDriverVacationStorage, DriverVacationStorage>();
builder.Services.AddScoped<DriverVacationLogic>();
builder.Services.AddScoped<IRepairCardStorage, RepairCardStorage>();
builder.Services.AddScoped<RepairCardLogic>();
builder.Services.AddScoped<ITransportWorkShiftStorage, TransportWorkShiftStorage>();
builder.Services.AddScoped<TransportWorkShiftLogic>();
builder.Services.AddScoped<IRentStorage, RentStorage>();
builder.Services.AddScoped<RentLogic>();
builder.Services.AddScoped<ITransportDutyShiftStorage, TransportDutyShiftStorage>();
builder.Services.AddScoped<TransportDutyShiftLogic>();
builder.Services.AddScoped<HangfireLogic>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseStatusCode | HttpLoggingFields.ResponseBody;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
//builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());

var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    )
};

//!_! ------------------ ScheduleService
var scheduleAddress = new Uri(builder.Configuration["ScheduleSettings:BaseAddress"]);
builder.Services.AddRefitClient<IScheduleServiceApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = scheduleAddress);

//!_! ------------------ Auth
var authAddress = new Uri(builder.Configuration["AuthSettings:BaseAddress"]);
builder.Services.AddRefitClient<IAuthApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = authAddress);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var dbLogs = scope.ServiceProvider.GetRequiredService<TransportDbContext>();
    dbLogs.Database.Migrate();
}
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetService<HangfireLogic>();
    //â 01:15
    RecurringJob.AddOrUpdate(() => service.FillTransportNextWorkDateAsync(DateTime.Now), "15 01 * * *", TimeZoneInfo.Utc);
    //BackgroundJob.Enqueue(() => service.FillTransportNextWorkDateAsync(DateTime.Now.AddDays(2)));
}

app.Run();
