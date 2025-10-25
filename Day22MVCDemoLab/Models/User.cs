using System.ComponentModel.DataAnnotations;

namespace MVCDemoLabpart1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Username")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        public string Email { get; set; }
    }
}
