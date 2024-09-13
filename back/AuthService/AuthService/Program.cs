using AuthService.Data;
using AuthService.Models;
using AuthService.Models.Settings;
using AuthService.Services;
using GreenCity.Logging.DbLogging;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthService;
using AuthService.Services.Interface;
using AuthService.Repository;
using AuthService.MappingProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GreenCity.Db.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AuthMappingProfile>());

builder.Services.AddScoped<IDbLogging, DbLogging>();
builder.Services.AddScoped<FillHistoryService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IUserToOrgsRepository, UserToOrgsRepository>();
builder.Services.AddScoped<IOrganizationManager, OrganizationManager>();

var connectionString = GlobalSettings.IsUnitTesting ? "ConnectionStringUT" : "ConnectionString";

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerDb:{connectionString}"]));

builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration[$"MsSqlServerLogsDb:{connectionString}"]));

builder.Services.AddIdentity<GreenCityIdentityUser, GreenCityIdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RoleInitSettings>(builder.Configuration.GetSection("RoleInitSettings"));

builder.Services.AddSingleton<IGenerateJwtService, GenerateJwtService>();

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
    //{
    //    Predicate = (check) => check.Tags.Contains("ready"),
    //    ResponseWriter = async (context, report) =>
    //    {
    //        var result = JsonSerializer.Serialize(
    //            new
    //            {
    //                status = report.Status.ToString(),
    //                checks = report.Entries.Select(entry => new
    //                {
    //                    name = entry.Key,
    //                    status = entry.Value.Status.ToString(),
    //                    exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
    //                    duration = entry.Value.Duration.ToString()
    //                })
    //            }
    //        );

    //        context.Response.ContentType = MediaTypeNames.Application.Json;
    //        await context.Response.WriteAsync(result);
    //    }
    //});

    //endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    //{
    //    AllowCachingResponses = false,
    //    Predicate = (_) => false
    //});
});

using (var scope = app.Services.CreateScope())
{
    var authDbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    authDbContext.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<GreenCityIdentityUser>>();
    var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<GreenCityIdentityRole>>();
    await RoleInitializerService.InitializeAsync(userManager, rolesManager);
}
app.Run();
