using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Following
    {
        public int Id { get; set; }

        public int? FollowerProfileId { get; set; }

        public Profile FollowerProfile { get; set; }

        public int? FollowingProfileId { get; set; }

        public Profile FollowingProfile { get; set; }

    }
}
