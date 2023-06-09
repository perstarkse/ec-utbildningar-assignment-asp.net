﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "You must enter a email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set;} = false;
    }
}
