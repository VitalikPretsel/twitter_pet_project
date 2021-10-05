using System;

namespace DAL.ViewModels
{
    public class PostVm
    {
        public int Id { get; set; }
        public string PostText { get; set; }
        public DateTime PostingDate { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int LikesAmount { get; set; }
        public int RepliesAmount { get; set; }
    }
}
