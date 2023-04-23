using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Models.Entities;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class ProductsService
    {
        private readonly DataContext _context;
        public ProductsService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfProductExists(Expression<Func<ProductEntity, bool>> predicate)
        {
            if (await _context.Products.AnyAsync(predicate))
            {
                return true;
            }
            return false;
        }

        public async Task<ProductEntity> GetAllAsync(Expression<Func<ProductEntity, bool>> predicate)
        {
            var productEntity = await _context.Products.FirstOrDefaultAsync(predicate);
            return productEntity!;
        }
        public async Task<List<CategoryEntity>> GetAllCategoriesAsync()
        {
            var dbItems = await _context.Categories.ToListAsync();
        
            return dbItems;
        }

        public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = new List<ProductModel>();
            var dbItems = await _context.Products
                .Include(p => p.ProductCategories)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();

            foreach (var item in dbItems)
            {
                ProductModel productModel = item;
                products.Add(productModel);
            }

            return products;
        }

        public async Task<List<ProductEntity>> GetProductEntitiesAsync()
        {
            var dbItems = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

            return dbItems;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var products = new List<ProductModel>();
            var dbItems = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

            foreach (var item in dbItems)
            {
                ProductModel productModel = item;
                products.Add(productModel);
            }

            return products;
        }
        public async Task<bool> CreateProductAsync(ProductRegistrationViewModel productRegistrationViewModel)
        {
            try
            {
                ProductEntity productEntity = productRegistrationViewModel;
                _context.Products.Add(productEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ProductEntity> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }
        public async Task<bool> SaveProductAsync(ProductEntity productEntity)
        {
            try
            {
                _context.Products.Update(productEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<CategoryEntity> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> CreateCategoryAsync(string categoryName)
        {
            try
            {
                var newCategory = new CategoryEntity { Name = categoryName };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCategoryAsync(CategoryEntity category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateProductCategoriesAsync(ProductEntity product, List<int> newCategoryIds)
        {
            // Remove old relationships
            _context.ProductCategories.RemoveRange(product.ProductCategories);

            // Add new relationships
            foreach (var categoryId in newCategoryIds)
            {
                product.ProductCategories.Add(new ProductCategoryEntity
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                });
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
