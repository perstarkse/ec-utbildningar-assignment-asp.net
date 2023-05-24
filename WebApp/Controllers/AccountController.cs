using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.Services;
using System.Security.Claims;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;


        public AccountController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _userService.GetProfileByUserIdAsync(userId);

            if (profile == null)
            {
                return View("Error");
            }

            var isAdmin = User.IsInRole("admin");

            var userProfileViewModel = new UserProfileViewModel
            {
                FullName = $"{profile.FirstName} {profile.LastName}",
                Email = profile.User.Email,
                PhoneNumber = profile.PhoneNumber,
                ProfileImage = profile.ProfileImage,
                Company = profile.Company,
                StreetName = profile.StreetName,
                City = profile.City,
                PostalCode = profile.PostalCode,
                IsAdmin = isAdmin
            };

            return View(userProfileViewModel);
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
            RegisterViewModel SignUp = new RegisterViewModel(){
                Title = "Please Register Your New Account",
            };
            return View(SignUp);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel) {
            if (ModelState.IsValid)
            {
                if (await _authService.SignUpAsync(registerViewModel))
                {
                    return RedirectToAction("SignIn", "Account");
                }

                ModelState.AddModelError("", "Something went wrong creating a new user, please try again"); 
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
