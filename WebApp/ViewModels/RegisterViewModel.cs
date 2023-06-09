﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Net;
using WebApp.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.ViewModels
{
    public class RegisterViewModel

	{
        public string? Title { get; set; }

        [Required(ErrorMessage = "You must enter a first name")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\u00C0-\u00FF'-]+$", ErrorMessage = "A valid name can only contain letters, spaces, apostrophes, and hyphens")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a last name")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\u00C0-\u00FF'-]+$", ErrorMessage = "A valid name can only contain letters, spaces, apostrophes, and hyphens")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password needs to have at least 8 characters, one uppercase letter, and one lowercase letter")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "You must confirm your password")]
        [Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } = null!;

		[Display(Name = "Profile image")]
		public string? ProfileImage { get; set; }

        [Display(Name = "Street name")]
        public string? StreetName { get; set; }

        [Display(Name = "Postal code")]
        public string? PostalCode { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Company")]
        public string? Company { get; set; }

        public bool RequireTerms { get; set; } = true;


        public static implicit operator IdentityUser(RegisterViewModel registerViewModel)
        {
            return new IdentityUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
            };
        }

        public static implicit operator ProfileEntity(RegisterViewModel registerViewModel)
        {
            return new ProfileEntity()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                PhoneNumber = registerViewModel.PhoneNumber,
                ProfileImage = registerViewModel.ProfileImage,
                Company = registerViewModel.Company,
                StreetName = registerViewModel.StreetName,
                PostalCode = registerViewModel.PostalCode,
                City = registerViewModel.City,
            };
        }
    }
}
