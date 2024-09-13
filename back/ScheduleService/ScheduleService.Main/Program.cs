using GreenCity.CommonUtils.CommonSettings;
using GreenCity.Logging.DbLogging;
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
using ScheduleService.Core.BusinessLogic;
using ScheduleService.Core.Services.ContractSchedule;
using ScheduleService.Core.Services.DateRange;
using ScheduleService.Database;
using ScheduleService.Database.Implements;
using ScheduleService.Database.Interfaces;
using ScheduleService.Database.MappingProfile;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var connectionString = "ConnectionString";

builder.Services.AddDbContext<ScheduleDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerDb:{connectionString}"]));

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));

// Add services to the container.

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

builder.Services.AddScoped<IDbLogging, DbLogging>();

builder.Services.AddScoped<IRedDatesStorage, RedDatesStorage>();
builder.Services.AddScoped<IWeekendWorkDatesStorage, WeekendWorkDatesStorage>();

builder.Services.AddScoped<IContractScheduleService, ContractScheduleService>();
builder.Services.AddScoped<IDateRangeSlicer, DateRangeSlicer>();

builder.Services.AddScoped<DatesLogic>();
builder.Services.AddScoped<CronScheduleLogic>();
builder.Services.AddScoped<TimeRangeLogic>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "ScheduleService", Version = "v1" });

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1 == 1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region Run

using (var scope = app.Services.CreateScope())
{
    var dbLogs = scope.ServiceProvider.GetRequiredService<ScheduleDbContext>();
    dbLogs.Database.Migrate();
}
app.Run();

#endregion