namespace WebApp.Models.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<ProductCategoryEntity> ProductCategories { get; set; } = new List<ProductCategoryEntity>();
    }
}
