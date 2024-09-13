using AuthContracts.DataContracts;
using AuthService.Models;
using AuthService.Services.Interface;
using AuthService.ViewModels;
using GreenCity.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<GreenCityIdentityUser> _userManager;
        private readonly SignInManager<GreenCityIdentityUser> _signInManager;
        private readonly IGenerateJwtService _jwtGenerator;

        public AccountController(
            UserManager<GreenCityIdentityUser> userManager,
            SignInManager<GreenCityIdentityUser> signInManager,
            IGenerateJwtService jwtGenerator
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new GreenCityIdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    GreenCityUserName = model.GreenCityUserName
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");

                }
            }

            var result2 = ModelState.GetBadRequestWithErrors<AuthUserResult>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Token()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                var token = _jwtGenerator.GenerateJwtToken(user, await _userManager.GetRolesAsync(user));

                return Json($"Bearer {token}");
            }

            return Unauthorized();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
