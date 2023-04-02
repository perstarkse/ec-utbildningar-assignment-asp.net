using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var viewmodel = new HomeIndexViewModel
        {
			BestCollection = new GridCollectionViewModel
            {
				Title = "Best Collection"
			}
		};

        return View(viewmodel);
    }
}
