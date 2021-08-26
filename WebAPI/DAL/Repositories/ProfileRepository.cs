using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationContext context) : base(context)
        {

        }

        public Profile GetByProfileName(string profileName)
        {
            return appContext.Profiles.FirstOrDefault(p => p.ProfileName == profileName);
        }
    }
}
