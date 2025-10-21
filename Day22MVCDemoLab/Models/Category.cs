using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Day22MVCDemoLab.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Must Enter Name...")]
        [MaxLength(150 , ErrorMessage = "Name cannot be more than 150 characters...")]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<Products> Products { get; set; } = new HashSet<Products>(); // here I make it HashSet to avoid null reference exceptions


    }
}
