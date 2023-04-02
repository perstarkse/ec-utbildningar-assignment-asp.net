using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
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
    }
}
