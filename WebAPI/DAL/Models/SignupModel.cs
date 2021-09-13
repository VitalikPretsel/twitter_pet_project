using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class SignupModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
