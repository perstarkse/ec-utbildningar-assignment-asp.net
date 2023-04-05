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
				Title = "Best Collection",
                Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptop", "Mobile", "Beauty" },
			    GridItems = new List<GridCollectionItemViewModel>
                {
                    new GridCollectionItemViewModel {Id = "1", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
                    new GridCollectionItemViewModel {Id = "2", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "3", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "4", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "5", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "6", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "7", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
					new GridCollectionItemViewModel {Id = "8", Title ="Deez fine ass shoes", Price="30", ImageUrl="images/placeholders/270x295.svg"},
				}
			}
		};

        return View(viewmodel);
    }
}