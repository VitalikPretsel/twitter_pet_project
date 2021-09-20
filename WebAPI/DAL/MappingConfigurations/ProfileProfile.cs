using AutoMapper;
using DAL.Entities;
using DAL.ViewModels;

namespace DAL.MappingConfigurations
{
    class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Entities.Profile, ProfileViewModel>();
        }
    }
}
