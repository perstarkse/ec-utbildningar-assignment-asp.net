using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.Entities;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ProductsService _productsService;

    public HomeController(ProductsService productsService)
    {
        _productsService = productsService;
    }

    public async Task<IActionResult> Index()
    {
        List<ProductModel> popularProducts = null;
        List<ProductModel> newProducts = null;
        List<ProductModel> featuredProducts = null;
        var popularId = await _productsService.GetCategoryByNameAsync("Popular");
		var newProductsId = await _productsService.GetCategoryByNameAsync("New");
        var featuredId = await _productsService.GetCategoryByNameAsync("Featured");
        if (popularId != null)
        {
            popularProducts = await _productsService.GetProductsByCategoryIdAsync(popularId.Id);
        }
        if (newProductsId != null)
        {
            newProducts = await _productsService.GetProductsByCategoryIdAsync(newProductsId.Id);
        }
        if (featuredId != null)
        {
            featuredProducts = await _productsService.GetProductsByCategoryIdAsync(featuredId.Id);
        }

        var viewmodel = new HomeIndexViewModel
		{
			BestCollection = new GridCollectionViewModel
			{
				Title = "Best Collection",
				Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptop", "Mobile", "Beauty" },
				GridItems = new List<GridCollectionItemViewModel>
				{
					new GridCollectionItemViewModel { Id = "1", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "2", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "3", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "4", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "5", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "6", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "7", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
					new GridCollectionItemViewModel { Id = "8", Title = "Deez fine ass shoes", Price = "30", ImageUrl = "images/placeholders/270x295.svg" },
				}
			},

			TopSelling = new GridCollectionViewModel
			{
				Title = "Popular products",
				GridItems = new List<GridCollectionItemViewModel>(),
				LoadMore = true,
            },
			New = new GridCollectionViewModel
			{ 
				Title = "New Products",
				GridItems = new List<GridCollectionItemViewModel>(),
				LoadMore = true,
			},
            Featured = new GridCollectionViewModel
            {
                Title = "Featured Products",
                GridItems = new List<GridCollectionItemViewModel>(),
                LoadMore = true,
            },
            PostShowcase = new PostShowcaseViewModel
            {
                Posts = new List<PostShowcaseItemViewModel>
                {
                    new PostShowcaseItemViewModel {Id = "1", Title = "This is the first admin post", Author = "Admin", Comments = 13, Description = "Welcome to the grand adventure that is bmerkato", Image = "images/placeholders/370x295.svg" },
                    new PostShowcaseItemViewModel {Id = "1", Title = "This is the first admin post", Author = "Admin", Comments = 13, Description = "Welcome to the grand adventure that is bmerkato", Image = "images/placeholders/370x295.svg" },
                    new PostShowcaseItemViewModel {Id = "1", Title = "This is the first admin post", Author = "Admin", Comments = 13, Description = "Welcome to the grand adventure that is bmerkato", Image = "images/placeholders/370x295.svg" },
                }
            }
        };

		if (popularProducts != null)
		{
			foreach (var product in popularProducts)
			{
				viewmodel.TopSelling.GridItems.Add(new GridCollectionItemViewModel
				{
					Id = product.Id.ToString(),
					Title = product.Name,
					Price = product.Price.ToString(),
					ImageUrl = product.ImageUrl != null ? product.ImageUrl : "images/placeholders/270x295.svg"
				});
			}
		}
        if (newProducts != null)
        {
            foreach (var product in newProducts)
            {
                viewmodel.New.GridItems.Add(new GridCollectionItemViewModel
                {
                    Id = product.Id.ToString(),
                    Title = product.Name,
                    Price = product.Price.ToString(),
                    ImageUrl = product.ImageUrl != null ? product.ImageUrl : "images/placeholders/270x295.svg"
                });
            }
        }
        if (featuredProducts != null)
        {
            foreach (var product in featuredProducts)
            {
                viewmodel.Featured.GridItems.Add(new GridCollectionItemViewModel
                {
                    Id = product.Id.ToString(),
                    Title = product.Name,
                    Price = product.Price.ToString(),
                    ImageUrl = product.ImageUrl != null ? product.ImageUrl : "images/placeholders/270x295.svg"
                });
            }
        }


        return View(viewmodel);
    }
}