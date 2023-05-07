namespace WebApp.ViewModels
{
	public class HomeIndexViewModel
	{
		public string Title { get; set; } = "Home";
		public GridCollectionViewModel BestCollection { get; set; } = null!;
		public GridCollectionViewModel TopSelling { get ; set; } = null!;
        public GridCollectionViewModel New { get; set; } = null!;
        public GridCollectionViewModel Featured { get; set; } = null!;
		public PostShowcaseViewModel PostShowcase { get; set; } = null!;
    }
}
