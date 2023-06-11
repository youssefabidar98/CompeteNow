using CompeteNow.Models;
using CompeteNow.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompeteNow.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await accountService.SigninAsync(model.Email, model.Password, model.BirthDate, model.Genre);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        //public IActionResult Login(string email, string password, bool rememberme)
        {
            try { 
                if(!ModelState.IsValid)
                {
                    return View();
                }
                await accountService.LoginAsync(model.Email, model.Password, model.RememberMe);
            
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
