using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class UserProfileMapping: AutoMapper.Profile
    {
        public UserProfileMapping()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
