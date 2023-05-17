using System.ComponentModel.DataAnnotations;

public class ContactFormViewModel
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Mobile { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Company { get; set; }

    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; }

    [Required(ErrorMessage = "Please accept the terms.")]
    public bool Terms { get; set; }
}