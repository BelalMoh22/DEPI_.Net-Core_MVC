using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Models
{
    public class ApplicationUser:IdentityUser //<int> // Custom ApplicationUser class inheriting from IdentityUser with int as the key type
    {
        [StringLength(100)]
        public string? CustomProperty { get; set; } // Example of a custom property
    }
}