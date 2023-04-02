using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";

        var showcase = new ShowcaseViewModel()
        {
            Ingress = "WELCOME TO BMERKETO SHOP",
            Title = "Exclusive Chair Gold Collection",
            LinkUrl = "#",
            LinkContent = "SHOP NOW",
            ImageUrl = "images/placeholders/625x647.svg",
        };

        return View(showcase);
    }
}
