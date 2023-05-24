using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Entities
{
    public class ProfileEntity 
    { 
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public string? Company { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public IdentityUser User { get; set; } = null!;
    }
}
