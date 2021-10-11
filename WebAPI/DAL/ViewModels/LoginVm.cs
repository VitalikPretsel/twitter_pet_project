using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MaxLength(256)]
        public string Login { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
