using System.ComponentModel.DataAnnotations.Schema;
using WebApp.ViewModels;

namespace WebApp.Models.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<ProductCategoryEntity> ProductCategories { get; set; } = new List<ProductCategoryEntity>();

        public static implicit operator ProductModel(ProductEntity productEntity)
        {
            return new ProductModel
            {
                Id= productEntity?.Id,
                Name = productEntity?.Name,
                Description = productEntity?.Description,
                Price = productEntity?.Price,
                ImageUrl = productEntity?.ImageUrl
            };
        }
    }
}
