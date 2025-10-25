using System.ComponentModel.DataAnnotations;

namespace MVCDemoLabpart1.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please Enter Username")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
