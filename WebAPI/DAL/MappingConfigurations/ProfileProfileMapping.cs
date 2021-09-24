using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class ProfileProfileMapping : AutoMapper.Profile
    {
        public ProfileProfileMapping()
        {
            CreateMap<Entities.Profile, ProfileVm>()
                .ForMember(p => p.PostsAmount, opt => opt.MapFrom(p => p.Posts.Count))
                .ForMember(p => p.FollowersAmount, opt => opt.MapFrom(p => p.Followers.Count))
                .ForMember(p => p.FollowingsAmount, opt => opt.MapFrom(p => p.Followings.Count));
        }
    }
}
