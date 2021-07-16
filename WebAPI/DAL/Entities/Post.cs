using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(140)]
        public string PostText { get; set; }

        [Required]
        public DateTime PostingDate { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }

        public List<Reply> Replies { get; set; }

        public List<Like> Likes { get; set; }
    }
}
