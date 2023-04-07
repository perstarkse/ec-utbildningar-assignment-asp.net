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

        public static implicit operator ProductEntity(ProductRegistrationViewModel productRegistrationViewModel)
        {
            return new ProductEntity
            {
                Name = productRegistrationViewModel.Name,
                Description = productRegistrationViewModel.Description,
                Price = productRegistrationViewModel.Price,
            };
        }
    }
}
