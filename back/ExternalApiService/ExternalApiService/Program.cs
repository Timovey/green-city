using AuthContracts.Interfaces;
using ExternalApiService.MappingProfiles;
using ExternalApiService.Operations;
using GreenCity.Logging.DbLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using SDContracts.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using ExternalApiService;
using ExternalApiService.Operations.Interface;
using TransportContracts.Interfaces;
using ScheduleService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FileService.Contracts.Interfaces;
using ExternalApiService.Services;
using ExternalApiService.Settings;
using LogistService.Contracts.Interfaces;
using DispatcherService.Contracts.Interfaces;
using MissionService.Contracts.Interface;
using ReportService.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<DeployOptions>(builder.Configuration);

// Add services to the container.

var connectionString = GlobalSettings.IsUnitTesting ? "ConnectionStringUT" : "ConnectionString";

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "ExternalApiService", Version = "v1" });

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuerSigningKey = true
            };
        });

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<ISdOperations, SdOperations>();
builder.Services.AddScoped<ITransportOperations, TransportOperations>();
builder.Services.AddScoped<IScheduleOperations, ScheduleOperations>();
builder.Services.AddScoped<IFileOperations, FileOperations>();
builder.Services.AddScoped<IEnumOperations, EnumOperations>();
builder.Services.AddScoped<ICommonOperations, CommonOperations>();
builder.Services.AddScoped<IAuthOperations, AuthOperations>();
builder.Services.AddScoped<ILogistOperations, LogistOperations>();
builder.Services.AddScoped<IDispatcherOperations, DispatcherOperations>();
builder.Services.AddScoped<IAppOperations, AppOperations>();
builder.Services.AddScoped<IReportOperations, ReportOperations>();

builder.Services.AddScoped<ITestDataProvider, TestDataProvider>();

//!_! ------------------ Mapping Profiles
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AuthMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<SdMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TransportMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ScheduleMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<FileMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<LogistMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<DispatcherMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MissionMappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ReportMappingProfile>());

builder.Services.AddCors(x => x.AddDefaultPolicy(xx => { xx.AllowAnyOrigin(); xx.AllowAnyHeader(); }));

var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }
    )
};

//!_! ------------------ Auth
var authAddress = new Uri(builder.Configuration["AuthSettings:BaseAddress"]);
builder.Services.AddRefitClient<IAuthApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = authAddress);

//!_! ------------------ SD
var sdAddress = new Uri(builder.Configuration["SdSettings:BaseAddress"]);
builder.Services.AddRefitClient<IContractClientApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
builder.Services.AddRefitClient<IStandApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
builder.Services.AddRefitClient<IContractApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);
builder.Services.AddRefitClient<IContainerApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = sdAddress);

//!_! ------------------ ScheduleService
var scheduleAddress = new Uri(builder.Configuration["ScheduleSettings:BaseAddress"]);
builder.Services.AddRefitClient<IScheduleServiceApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = scheduleAddress);

//!_! ------------------ Transports
var transportAddress = new Uri(builder.Configuration["TransportSettings:BaseAddress"]);
builder.Services.AddRefitClient<ITransportApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = transportAddress);
builder.Services.AddRefitClient<IDriverApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = transportAddress);

//!_!------------------ Files
var fileAddress = new Uri(builder.Configuration["FileSettings:BaseAddress"]);
builder.Services.AddRefitClient<IFileApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = fileAddress);

//!_! ------------------ Logist
var logistAddress = new Uri(builder.Configuration["LogistSettings:BaseAddress"]);
builder.Services.AddRefitClient<IBaseRouteApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = logistAddress);
builder.Services.AddRefitClient<IRouteApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = logistAddress);

//!_! ------------------ Dispatcher
var dispatcherAddress = new Uri(builder.Configuration["DispatcherSettings:BaseAddress"]);
builder.Services.AddRefitClient<IDispatcherApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = dispatcherAddress);
builder.Services.AddRefitClient<IShiftTaskApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = dispatcherAddress);
builder.Services.AddRefitClient<ITravelSheetApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = dispatcherAddress);
builder.Services.AddRefitClient<IDoneTaskListApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = dispatcherAddress);

//!_! ------------------ Missions
var missionAddress = new Uri(builder.Configuration["MissionSettings:BaseAddress"]);
builder.Services.AddRefitClient<IAppApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = missionAddress);
builder.Services.AddRefitClient<IMissionApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = missionAddress);

//!_! ------------------ ReportService
var reportAddress = new Uri(builder.Configuration["ReportSettings:BaseAddress"]);
builder.Services.AddRefitClient<IReportApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = reportAddress);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseStatusCode | HttpLoggingFields.ResponseBody;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

//builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

