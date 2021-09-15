using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
