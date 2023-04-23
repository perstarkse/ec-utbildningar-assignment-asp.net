namespace WebApp.Models.Entities
{
    public class ProductCategoryEntity
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } 

        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
