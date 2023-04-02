using WebApp.ViewModels;

namespace WebApp.Services
{
    public class ShowcaseService
    {
        private List<ShowcaseModel> _showcases = new()
        {
			new ShowcaseModel()
			{
				Ingress = "WELCOME TO BMERKETO SHOP",
				Title = "Exclusive Chair Gold Collection",
				Button = new Models.LinkButtonModel
				{
					Content = "SHOP NOW",
					Url = "/products",
				},
				ImageUrl = "images/placeholders/625x647.svg",
			}
		};

        public ShowcaseModel GetLatest()
        {
            return _showcases.LastOrDefault()!;
        }
    }
}
