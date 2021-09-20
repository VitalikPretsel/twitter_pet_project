using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class PostProfile : AutoMapper.Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostViewModel>();
        }
    }
}
