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

        public IActionResult Index()
        {
            ViewData["Title"] = "Products";

            return View();
        }

        public IActionResult Search()
        {
            ViewData["Title"] = "Search for products";
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
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
