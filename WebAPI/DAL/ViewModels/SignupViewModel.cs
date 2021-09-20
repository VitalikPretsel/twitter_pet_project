using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(256)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
