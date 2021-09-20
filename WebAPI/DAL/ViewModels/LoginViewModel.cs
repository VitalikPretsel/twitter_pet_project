using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
