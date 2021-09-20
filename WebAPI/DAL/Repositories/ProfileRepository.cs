using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

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

        public string GetProfileName(int id)
        {
            return appContext.Profiles.Find(id).ProfileName;
        }

        public async Task<IEnumerable<Profile>> GetUserProfiles(int userId)
        {
            return await appContext.Profiles.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
