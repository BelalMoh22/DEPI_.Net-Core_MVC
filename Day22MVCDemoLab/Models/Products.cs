using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDemoLabpart1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Must Enter Name...")]
        [MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters...")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between 0.01 and 1,000,000.00")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [NotMapped] // This property is not mapped to the database
        public byte[]? Image { get; set; }

        [MaxLength(255)]
        public string? ImagePath { get; set; }

        [ForeignKey("Category")] // Specify the foreign key relationship
        public int CategoryId { get; set; } // FK

        public Category? Category { get; set; } // Navigation property
    }
}
