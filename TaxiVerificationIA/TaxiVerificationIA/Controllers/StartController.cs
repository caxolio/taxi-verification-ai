using Microsoft.AspNetCore.Mvc;

using TaxiVerificationIA.Models;
using TaxiVerificationIA.Resources;
using TaxiVerificationIA.Services.Contract;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TaxiVerificationIA.Services.Implementation;

namespace TaxiVerificationIA.Controllers
{
    public class StartController : Controller
    {
        private readonly IUserService _userService;

        public StartController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User userModel)
        {
            userModel.Password = Utilities.EncryptKey(userModel.Password);

            User user = await _userService.SaveUser(userModel);

            if (user.IdUser > 0)
                return RedirectToAction("SignIn", "Start");

            ViewData["Message"] = "No se pudo crear el Usuario";
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string pass)
        {
            User user = await _userService.GetUser(email, Utilities.EncryptKey(pass));

            if (user == null)
            {
                ViewData["Message"] = "El usuario no fue encontrado";
                return View();
            }

            var agents = user.Agents.ToList();

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.PrimarySid, agents[0].IdAgent.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties() { 
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Verifications", "Verifications");
        }
    }
}
