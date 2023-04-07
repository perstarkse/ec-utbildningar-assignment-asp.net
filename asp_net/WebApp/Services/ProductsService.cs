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

        public async Task<IEnumerable<ProductModel>> GetAllAsync ()
        {
            var products = new List<ProductModel>();
            var dbItems = await _context.Products.ToListAsync();
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
    }       
}
