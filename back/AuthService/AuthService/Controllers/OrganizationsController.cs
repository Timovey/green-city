using AuthContracts.Constants;
using AuthService.Models;
using AuthService.Models.Orgs;
using AuthService.Services.Interface;
using AuthService.ViewModels.Orgs;
using GreenCity.Http.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Authorize(Roles = AuthRolesConstants.ADMIN)]
    [ModelStateValidationActionFilter]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationManager _organizationManager;
        private readonly UserManager<GreenCityIdentityUser> _userManager;

        public OrganizationsController(
            IOrganizationManager organizationManager,
            UserManager<GreenCityIdentityUser> userManager
        )
        {
            _organizationManager = organizationManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index() => View(await _organizationManager.GetOrgListAsync());

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public IActionResult CreateOrg() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrgAsync(CreateOrgModel createModel)
        {
            var createdUserInfo = await _userManager.GetUserAsync(User);
            await _organizationManager.AddOrgAsync(createModel, createdUserInfo.Id, createdUserInfo.GreenCityUserName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditOrgAsync(int organizationId)
        {
            var org = await _organizationManager.GetOrgForEditByIdAsync(organizationId);
            if (org == default)
                return NotFound();

            return View(org);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrgAsync(EditOrgViewModel model)
        {
            var editUserInfo = await _userManager.GetUserAsync(User);
            await _organizationManager.UpdateOrgAsync(model, editUserInfo.Id, editUserInfo.GreenCityUserName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int organizationId)
        {
            var org = await _organizationManager.GetOrgByIdAsync(organizationId);
            if (org != null)
            {
                var deletedUserInfo = await _userManager.GetUserAsync(User);
                await _organizationManager.DeleteOrgAsync(organizationId, deletedUserInfo.Id, deletedUserInfo.GreenCityUserName);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUserOrgsAsync(string userId)
        {
            // получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            // получем список организаций пользователя
            var userOrgs = await _organizationManager.GetUserOrgsAsync(user.Id);
            var allOrgs = await _organizationManager.GetOrgListAsync();
            ChangeUserOrgViewModel model = new ChangeUserOrgViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserOrgs = userOrgs,
                AllOrgs = allOrgs
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserOrgsAsync(string userId, IList<int> actualUserOrgIds)
        {
            // получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var updatedUserInfo = await _userManager.GetUserAsync(User);

            await _organizationManager.CreateOrUpdateUserToOrgsAsync(
                user.Id, actualUserOrgIds, updatedUserInfo.Id, updatedUserInfo.GreenCityUserName
            );

            return RedirectToAction("UserList");
        }
    }
}
