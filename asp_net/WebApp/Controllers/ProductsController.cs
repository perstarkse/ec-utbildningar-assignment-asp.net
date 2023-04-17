using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService= productsService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "All Products";

            var products = await _productsService.GetAllAsync();

            var AllProducts = new GridCollectionViewModel
            {
                Title = "All Products",
                LoadMore = false,
                GridItems = new List<GridCollectionItemViewModel>()
            };
            foreach (var product in products)
            {
                AllProducts.GridItems.Add(new GridCollectionItemViewModel
                {
                    Id = product.Id.ToString(),
                    Title = product.Name,
                    Price = product.Price.ToString(),
                    ImageUrl = "images/placeholders/270x295.svg" 
                });
            }

            return View(AllProducts);
        }

        public IActionResult Search()
        {
            ViewData["Title"] = "Search for products";
            return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register()
        {
            var categories = await _productsService.GetAllCategoriesAsync();
            var viewModel = new ProductRegistrationViewModel
            {
                Categories = categories
            };
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await _productsService.CreateProductAsync(productRegistrationViewModel))
                {
                    return RedirectToAction("Register", "Products");
                }
                ModelState.AddModelError("", "Something went wrong when creating product");
            }
            return View();
        }
    }
}
