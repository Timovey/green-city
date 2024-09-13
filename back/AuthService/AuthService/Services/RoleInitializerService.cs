using AuthContracts.Constants;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class RoleInitializerService
    {
        /// <summary>Пароли берутся из окружения: секреты не хранятся в коде.</summary>
        private static string RequiredEnv(string name) =>
            Environment.GetEnvironmentVariable(name)
            ?? throw new InvalidOperationException($"Environment variable {name} is not set");

        public static async Task InitializeAsync(UserManager<GreenCityIdentityUser> userManager, RoleManager<GreenCityIdentityRole> roleManager)
        {
            //!_! Admin
            string adminEmail = "admin@gmail.com";
            string password = RequiredEnv("GREENCITY_ADMIN_PASSWORD");
            string greenCityUserName = "Just Admin";

            //!_! For unit tests
            string testUsersPassword = RequiredEnv("GREENCITY_TEST_USERS_PASSWORD");
            string testUser1Email = "barbosik@gmail.com";
            string testUser1Password = testUsersPassword;
            string testUser1Name = "Barbosik_logist";

            string testUser2Email = "antony@gmail.com";
            string testUser2Password = testUsersPassword;
            string testUser2Name = "Antony_tax";

            string testUser3Email = "chelik@gmail.com";
            string testUser3Password = testUsersPassword;
            string testUser3Name = "Chelik_dispatcher";

            string testUser4Email = "keksik@gmail.com";
            string testUser4Password = testUsersPassword;
            string testUser4Name = "Keksik_ao";

            string testUser5Email = "test_admin@gmail.com";
            string testUser5Password = testUsersPassword;
            string testUser5Name = "test_admin_admin";

            //!_! Add roles
            if (await roleManager.FindByNameAsync(AuthRolesConstants.ADMIN) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.ADMIN));
            }

            if (await roleManager.FindByNameAsync(AuthRolesConstants.TAX) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.TAX));
            }

            if (await roleManager.FindByNameAsync(AuthRolesConstants.DISPATCHER) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.DISPATCHER));
            }

            if (await roleManager.FindByNameAsync(AuthRolesConstants.LOGIST) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.LOGIST));
            }

            if (await roleManager.FindByNameAsync(AuthRolesConstants.AO) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.AO));
            }
			
			//!_! Для создания заявок в любое время
			if (await roleManager.FindByNameAsync(AuthRolesConstants.ABSOLUTE_APP_ACTION_ROLE) == null)
            {
                await roleManager.CreateAsync(new GreenCityIdentityRole(AuthRolesConstants.ABSOLUTE_APP_ACTION_ROLE));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new GreenCityIdentityUser { Email = adminEmail, UserName = adminEmail, GreenCityUserName = greenCityUserName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AuthRolesConstants.ADMIN);
                }
            }

            if (GlobalSettings.IsUnitTesting)
            {
                if (await userManager.FindByNameAsync(testUser1Email) == null)
                {
                    var user = new GreenCityIdentityUser { Email = testUser1Email, UserName = testUser1Email, GreenCityUserName = testUser1Name };
                    IdentityResult result = await userManager.CreateAsync(user, testUser1Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, AuthRolesConstants.LOGIST);
                    }
                }

                if (await userManager.FindByNameAsync(testUser2Email) == null)
                {
                    var user = new GreenCityIdentityUser { Email = testUser2Email, UserName = testUser2Email, GreenCityUserName = testUser2Name };
                    IdentityResult result = await userManager.CreateAsync(user, testUser2Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, AuthRolesConstants.TAX);
                    }
                }

                if (await userManager.FindByNameAsync(testUser3Email) == null)
                {
                    var user = new GreenCityIdentityUser { Email = testUser3Email, UserName = testUser3Email, GreenCityUserName = testUser3Name };
                    IdentityResult result = await userManager.CreateAsync(user, testUser3Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, AuthRolesConstants.DISPATCHER);
                    }
                }

                if (await userManager.FindByNameAsync(testUser4Email) == null)
                {
                    var user = new GreenCityIdentityUser { Email = testUser4Email, UserName = testUser4Email, GreenCityUserName = testUser4Name };
                    IdentityResult result = await userManager.CreateAsync(user, testUser4Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, AuthRolesConstants.AO);
                    }
                }

                if (await userManager.FindByNameAsync(testUser5Email) == null)
                {
                    var user = new GreenCityIdentityUser { Email = testUser5Email, UserName = testUser5Email, GreenCityUserName = testUser5Name };
                    IdentityResult result = await userManager.CreateAsync(user, testUser5Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, AuthRolesConstants.ADMIN);
                    }
                }
            }
        }
    }
}
