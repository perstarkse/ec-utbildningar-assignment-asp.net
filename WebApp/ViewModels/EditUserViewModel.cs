using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; } = new List<SelectListItem>();
    }
}
