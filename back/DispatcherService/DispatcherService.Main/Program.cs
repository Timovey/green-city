using AuthContracts.Interfaces;
using DispatcherService.Core.BusinessLogic;
using DispatcherService.Core.BusinessLogic.Interface;
using DispatcherService.Core.MappingProfile;
using DispatcherService.Core.Services;
using DispatcherService.Core.Services.Interface;
using DispatcherService.Database;
using DispatcherService.Database.Implements;
using DispatcherService.Database.Interfaces;
using DispatcherService.Database.MappingProfiles;
using GreenCity.Db.Services;
using GreenCity.Db.Services.History.Interface;
using GreenCity.Logging.DbLogging;
using LogistService.Contracts.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MissionService.Contracts.Interface;
using Refit;
using System;
using System.Text.Json;
using TransportContracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var connectionString = "ConnectionString";

builder.Services.AddDbContext<DispatcherDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerDb:{connectionString}"]));

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));


builder.Services.AddControllers();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ShiftTaskMappingProfile>());

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<IFillHistoryService, FillHistoryService>();

builder.Services.AddScoped<AuxiliaryWorkerLogic>();
builder.Services.AddScoped<IBrigadeLogic, BrigadeLogic>();
builder.Services.AddScoped<ITravelSheetLogic, TravelSheetLogic>();
builder.Services.AddScoped<IShiftTaskLogic, ShiftTaskLogic>();
builder.Services.AddScoped<IDoneTaskListLogic, DoneTaskListLogic>();

builder.Services.AddScoped<IBrigadeOperationsValidator, BrigadeOperationsValidator>();

builder.Services.AddScoped<IAuxiliaryWorkerStorage, AuxiliaryWorkerStorage>();
builder.Services.AddScoped<IBrigadeStorage, BrigadeStorage>();
builder.Services.AddScoped<ITravelSheetStorage, TravelSheetStorage>();
builder.Services.AddScoped<IShiftTaskStorage, ShiftTaskStorage>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "DispatcherService", Version = "v1" });

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

//!_! ------------------ TransportService
var transportAddress = new Uri(builder.Configuration["TransportSettings:BaseAddress"]);
builder.Services.AddRefitClient<ITransportApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = transportAddress);

builder.Services.AddRefitClient<IDriverApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = transportAddress);

//!_! ------------------ LogistService
var logistAddress = new Uri(builder.Configuration["LogistSettings:BaseAddress"]);
builder.Services.AddRefitClient<IRouteApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = logistAddress);

//!_! ------------------ AuthService
var authAddress = new Uri(builder.Configuration["AuthSettings:BaseAddress"]);
builder.Services.AddRefitClient<IAuthApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = authAddress);

//!_! ------------------ Missions
var missionAddress = new Uri(builder.Configuration["MissionSettings:BaseAddress"]);
builder.Services.AddRefitClient<IMissionApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = missionAddress);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbLogs = scope.ServiceProvider.GetRequiredService<DispatcherDbContext>();
    dbLogs.Database.Migrate();
}

app.Run();
