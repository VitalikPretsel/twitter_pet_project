using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
