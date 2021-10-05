using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class PostProfileMapping : AutoMapper.Profile
    {
        public PostProfileMapping()
        {
            CreateMap<Post, PostVm>()
                .ForMember(p => p.ProfileName, opt => opt.MapFrom(p => p.Profile.ProfileName))
                .ForMember(p => p.LikesAmount, opt => opt.MapFrom(p => p.Likes.Count))
                .ForMember(p => p.RepliesAmount, opt => opt.MapFrom(p => p.Replies.Count));
        }
    }
}
