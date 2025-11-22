using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "Department name is required.")]
        [MaxLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }
    }
}