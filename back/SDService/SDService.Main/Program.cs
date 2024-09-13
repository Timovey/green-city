using GreenCity.CommonUtils.CommonSettings;
using GreenCity.Db.Services;
using GreenCity.Logging.DbLogging;
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
using MissionService.Contracts.Interface;
using Refit;
using ScheduleService.Contracts.Interfaces;
using SDService.Core.BusinessLogic;
using SDService.Core.MappingProfiles;
using SDService.Core.Services.ContractSchedule;
using SDService.Database;
using SDService.Database.Implements;
using SDService.Database.Interfaces;
using SDService.Database.MappingProfile;
using SDService.Main;
using System;
using System.Text;
using System.Text.Json;

#region Startup

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var connectionString = GlobalSettings.IsUnitTesting ? "ConnectionStringUT" : "ConnectionString";

builder.Services.AddDbContext<SdDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerDb:{connectionString}"]));

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));


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
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MissionInfoMappingProfile>());

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<FillHistoryService>();

builder.Services.AddScoped<IStandStorage, StandStorage>();
builder.Services.AddScoped<StandLogic>();

builder.Services.AddScoped<IContractClientStorage, ContractClientStorage>();
builder.Services.AddScoped<ContractClientLogic>();

builder.Services.AddScoped<IContractStorage, ContractStorage>();
builder.Services.AddScoped<IContainer2ScheduleStorage, Container2ScheduleStorage>();
builder.Services.AddScoped<IContractScheduleMissionStorage, ContractScheduleMissionStorage>();
builder.Services.AddScoped<IMissionInfoStorage, MissionInfoStorage>();
builder.Services.AddScoped<Container2ScheduleLogic>();
builder.Services.AddScoped<ContractLogic>();

builder.Services.AddScoped<IContainerStorage, ContainerStorage>();
builder.Services.AddScoped<ContainerLogic>();

builder.Services.AddScoped<IMissionInfoStorage, MissionInfoStorage>();
builder.Services.AddScoped<MissionInfoLogic>();

builder.Services.AddScoped<IContractScheduleJobService, ContractScheduleJobService>();
builder.Services.AddSingleton<MissionSingletonLogic>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "SDService", Version = "v1" });

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

var scheduleAddress = new Uri(builder.Configuration["ScheduleSettings:BaseAddress"]);
builder.Services.AddRefitClient<IScheduleServiceApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = scheduleAddress);

var logistAddress = new Uri(builder.Configuration["LogistSettings:BaseAddress"]);
builder.Services.AddRefitClient<IBaseRouteApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = logistAddress);

var missionAddress = new Uri(builder.Configuration["MissionSettings:BaseAddress"]);
builder.Services.AddRefitClient<IMissionApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = missionAddress);

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
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

#region Run

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SdDbContext>();
    dbContext.Database.Migrate();
}
app.Run();

#endregion