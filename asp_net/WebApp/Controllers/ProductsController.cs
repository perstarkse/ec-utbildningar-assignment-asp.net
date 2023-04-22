using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Entities;
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProductAdministration()
        {
            var products = await _productsService.GetAllAsync();
            var categories = await _productsService.GetAllCategoriesAsync();

            var viewModel = new ProductAdminViewModel
            {
                Products = products.ToList(),
                Categories = categories.ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductEntity editProductViewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the product and update the changes
                var product = await _productsService.GetProductByIdAsync(editProductViewModel.Id);
                if (product != null)
                {
                    product.Name = editProductViewModel.Name;
                    product.Price = editProductViewModel.Price;
                    product.Description = editProductViewModel.Description;
                    product.Category = editProductViewModel.Category;
                    product.ImageUrl = editProductViewModel.ImageUrl;

                    if (await _productsService.SaveProductAsync(product)) 
                    {
                        return RedirectToAction("ProductAdministration");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something went wrong when saving the product");
                    }
                }
            }

            return BadRequest("Failed to edit the product");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CategoryManagement()
        {
            var categories = await _productsService.GetAllCategoriesAsync();

            var viewModel = new CategoryManagementViewModel
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCategory(int id, CategoryEntity category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await _productsService.UpdateCategoryAsync(category);

            return RedirectToAction(nameof(CategoryManagement));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _productsService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(CategoryManagement));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCategory(CategoryManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _productsService.CreateCategoryAsync(model.NewCategoryName))
                {
                    model.NewCategoryName = string.Empty;
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong when creating the category");
                }
            }

            // Get the updated list of categories.
            model.Categories = await _productsService.GetAllCategoriesAsync();

            return View("CategoryManagement", model);
        }


    }
}
