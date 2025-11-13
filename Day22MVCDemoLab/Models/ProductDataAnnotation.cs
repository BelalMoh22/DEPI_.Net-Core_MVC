using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Separate class for Data Annotations For Keeping it 
namespace MVCDemoLabpart1.Models
{
    // in Database First Approach, we create a separate class for Data Annotations
    [ModelMetadataType(typeof(ProductDataAnnotation))]
    public partial class Products
    {
        // Data Annotations can be added here
    }
    public class ProductDataAnnotation
    {
        [Required(ErrorMessage = "Must Enter Name...")]
        [MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters...")]
        public string Name { get; set; }

        //[Price(ErrorMessage ="The Price Must be at Least 500")]
        //or
        //[CustomValidation(typeof(PriceAttribute), "ValidatePrice", ErrorMessage = "The Price Must be at Least 500")] // Static Validation
        [Required]
        // Remote Validation
        [Remote("CheckPrice","WizardProducts", ErrorMessage = "The Price Must be at Least 1000")] // Finction in ProductsWizard Controller
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [NotMapped] // This property is not mapped to the database
        public byte[]? Image { get; set; }

        [MaxLength(255)]
        [DisplayName("Photo")]
        public string? ImagePath { get; set; }

        [ForeignKey("Category")] // Specify the foreign key relationship
        public int CategoryId { get; set; } // FK
    }
}
