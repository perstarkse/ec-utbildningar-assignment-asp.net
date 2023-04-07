using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
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
        public async Task<IActionResult> SignUp(ViewModels.RegisterViewModel registerViewModel) {
            if (ModelState.IsValid)
            {
                if (await _userService.CheckIfUserExists(x => x.Email == registerViewModel.Email))
                {
                    ModelState.AddModelError("", "There is already a user with the same email");
                    return View(registerViewModel);
                }
                else
                {
                    if (await _userService.RegisterAsync(registerViewModel))
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    ModelState.AddModelError("", "Something went wrong when creating user and profile");
                }
            }
            return View(registerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(ViewModels.LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _userService.LogInAsync(logInViewModel))
                {
                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(logInViewModel);
        }
    }
}
