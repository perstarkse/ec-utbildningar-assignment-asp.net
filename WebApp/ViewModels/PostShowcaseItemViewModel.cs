using WebApp.Models;

namespace WebApp.ViewModels
{
    public class PostShowcaseItemViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Comments { get; set; }
        public string? Image { get; set; }
        public string Author { get; set; } = null!;
    }
}
