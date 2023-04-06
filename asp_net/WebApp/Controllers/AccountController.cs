using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
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
                try
                {
                    if (!await _dataContext.Users.AnyAsync(x => x.Email == registerViewModel.Email))
                    {
                        UserEntity userEntity = registerViewModel;
                        ProfileEntity profileEntity = registerViewModel;

                        _dataContext.Users.Add(userEntity);
                        await _dataContext.SaveChangesAsync();

                        profileEntity.UserId = userEntity.Id;
                        _dataContext.Profiles.Add(profileEntity);
                        await _dataContext.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "There is already a user with the same email");
                }
                catch
                {
                    ModelState.AddModelError("", "Something went wrong when creating user and profile");
                }
            }
            return View(registerViewModel);
        }
    }
}
