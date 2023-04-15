using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["Title"] = "My Account";
            return View();
        }

		[Authorize]
		public new async Task<IActionResult> SignOut()
		{
            if (await _authService.SignOutAsync(User))
                return RedirectToAction("Index", "Home");
			return View();
		}

		public IActionResult SignIn()
        {
            ViewData["Title"] = "Login";
            return View();
        }
        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign up";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel) {
            if (ModelState.IsValid)
            {
                if (await _authService.SignUpAsync(registerViewModel))
                {
                    return RedirectToAction("SignIn", "Account");
                }

                ModelState.AddModelError("", "A user with the same email already exists"); 
            }
            return View(registerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _authService.SignInAsync(signInViewModel))
                {
                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(signInViewModel);
        }
    }
}
