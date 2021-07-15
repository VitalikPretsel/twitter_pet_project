using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Following
    {
        public int Id { get; set; }

        //[Required]
        public int? FollowerProfileId { get; set; }
        [ForeignKey("FollowerProfileId")]
        public Profile FollowerProfile { get; set; }
        //[Required]
        public int? FollowingProfileId { get; set; }
        [ForeignKey("FollowingProfileId")]
        public Profile FollowingProfile { get; set; }

    }
}
