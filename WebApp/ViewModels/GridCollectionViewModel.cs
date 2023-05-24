namespace WebApp.ViewModels
{
	public class GridCollectionViewModel
	{
		public string? Title { get; set; }
		public IEnumerable<string>? Categories { get; set; } = null!;
		public List<GridCollectionItemViewModel> GridItems { get; set; } = null!;
		public bool LoadMore { get; set; } = false;
	}
}
