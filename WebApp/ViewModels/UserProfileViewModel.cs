namespace WebApp.ViewModels
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public string? Company { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public bool IsAdmin { get; set; }
    }
}
