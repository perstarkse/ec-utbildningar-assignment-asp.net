using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CreateUserAsAdminViewModel
    {
        [Required(ErrorMessage = "You must enter a first name")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zA-Z\u00C0-\u00FF\s'-]+$", ErrorMessage = "A valid name can only contain letters, spaces, apostrophes, and hyphens")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a last name")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-zA-Z\u00C0-\u00FF\s'-]+$", ErrorMessage = "A valid name can only contain letters, spaces, apostrophes, and hyphens")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter an email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "You must confirm your password")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "You must select a role")]
        [Display(Name = "Role")]
        public string UserRole { get; set; } = null!;
    }
}
