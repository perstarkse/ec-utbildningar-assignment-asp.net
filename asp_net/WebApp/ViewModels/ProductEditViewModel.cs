namespace WebApp.ViewModels
{
    public class ProductEditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
        public string? ImageUrl { get; set; }
    }
}
