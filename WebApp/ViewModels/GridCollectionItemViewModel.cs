namespace WebApp.ViewModels
{
	public class GridCollectionItemViewModel
	{
		public string Id { get; set; } = null!;
		public string? Title { get; set; }
		public string? ImageUrl { get; set; }
		public string? Price { get; set; }
		public string? OldPrice { get; set; }
		public string? Discount { get; set; }
		public string? Rating { get; set; }
		public string? Reviews { get; set; }
		public string? Link { get; set; }
	}
}
