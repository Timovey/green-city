using GreenCity.CommonUtils.CommonSettings;
using GreenCity.Db.Services;
using GreenCity.Db.Services.History.Interface;
using GreenCity.Logging.DbLogging;
using Hangfire;
using LogistService.Contracts.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MissionService.Core.BusinessLogic;
using MissionService.Core.BusinessLogic.Interface;
using MissionService.Core.MappingProfile;
using MissionService.Database;
using MissionService.Database.MappingProfile;
using MissionService.Database.Storage;
using MissionService.Database.Storage.Interface;
using Refit;
using SDContracts.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using Hangfire.SqlServer;
using MissionService.Core.Services;
using MissionService.Core.Services.Interface;
using ScheduleService.Contracts.Interfaces;

#region Startup

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var connectionString = "ConnectionString";

builder.Services.AddDbContext<MissionDbContext>(options =>
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

builder.Services.AddControllers();

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<BusinessLogicMappingProfile>());

builder.Services.AddSingleton<IAppGeneratorSingletonService, AppGeneratorSingletonService>();

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<IFillHistoryService, FillHistoryService>();
builder.Services.AddScoped<IAppActionsValidationService, AppActionsValidationService>();

builder.Services.AddScoped<IMissionStorage, MissionStorage>();
builder.Services.AddScoped<IAppStorage, AppStorage>();
builder.Services.AddScoped<IMissionLogic, MissionLogic>();
builder.Services.AddScoped<IAppLogic, AppLogic>();
builder.Services.AddScoped<IHangfireLogic, HangfireLogic>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "MissionService", Version = "v1" });

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region Refit

var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    )
};

//!_! ------------------ SdService
var sdAddress = new Uri(builder.Configuration["SdSettings:BaseAddress"]);
builder.Services.AddRefitClient<IContainer2ScheduleApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);

builder.Services.AddRefitClient<IMissionInfoApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);

builder.Services.AddRefitClient<IContainerApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);

//!_! ------------------ LogistService
var logistAddress = new Uri(builder.Configuration["LogistSettings:BaseAddress"]);
builder.Services.AddRefitClient<IRouteApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = logistAddress);

//!_! ------------------ ScheduleService
var scheduleAddress = new Uri(builder.Configuration["ScheduleSettings:BaseAddress"]);
builder.Services.AddRefitClient<IScheduleServiceApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = scheduleAddress);

#endregion

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseStatusCode | HttpLoggingFields.ResponseBody;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

//builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());

#endregion

#region Pipeline

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

#region Run

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MissionDbContext>();
    dbContext.Database.Migrate();
}
//using (var scope = app.Services.CreateScope())
//{
//    var service = scope.ServiceProvider.GetService<IHangfireLogic>();
//    //� 01:00 ULN (UTC + 4)
//    //!_! Проваливаем сегодняшние и генерируем на завтра (если сегодня 21 число 1:00 (UTC 20 число 21:00), проваливаем все 20 числа по UTC те ВЧЕРА по ульяновскому)
//    //добавляем два дня, чтобы генерировалось на 22 число по UTC - что будет равно ульяновской дате 22 числа те ЗАВТРА
//    RecurringJob.AddOrUpdate(() => service.ExecAppDailyActionsAsync(DateTime.UtcNow.Date, DateTime.UtcNow.AddDays(2).Date), "00 21 * * *", TimeZoneInfo.Utc);
//}
app.Run();

#endregion
