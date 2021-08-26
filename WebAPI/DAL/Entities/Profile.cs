using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string ProfileName { get; set; }

        [MaxLength(256)]
        public string ProfileDescription { get; set; }

        public string ProfilePicturePath { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<Post> Posts { get; set; }

        public List<Reply> Replies { get; set; }

        public List<Like> Likes { get; set; }
    }
}