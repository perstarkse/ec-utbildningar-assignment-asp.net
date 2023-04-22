using System.ComponentModel.DataAnnotations;
using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.ViewModels
{
    public class ProductRegistrationViewModel
    {
        [Required(ErrorMessage = "You must enter a name")]
        [Display(Name = "Product Name *")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required(ErrorMessage = "You must enter a price")]
        [Display(Name = "Price *")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

        public static implicit operator ProductEntity(ProductRegistrationViewModel productRegistrationViewModel)
        {
            return new ProductEntity
            {
                Name = productRegistrationViewModel.Name,
                Description = productRegistrationViewModel.Description,
                Price = productRegistrationViewModel.Price,
                CategoryId = productRegistrationViewModel?.CategoryId,
                ImageUrl = productRegistrationViewModel?.ImageUrl
            };
        }
    }
}
