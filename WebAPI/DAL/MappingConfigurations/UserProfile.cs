using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class UserProfile: AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
