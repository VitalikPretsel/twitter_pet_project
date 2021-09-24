namespace DAL.ViewModels
{
    public class ProfileVm
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDescription { get; set; }
        public string ProfilePicturePath { get; set; }
        public int UserId { get; set; }
        public int FollowersAmount { get; set; }
        public int FollowingsAmount { get; set; }
        public int PostsAmount { get; set; }
    }
}
