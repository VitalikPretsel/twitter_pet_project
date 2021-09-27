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

        public new async Task<IEnumerable<Profile>> GetAll()
        {
            return await appContext.Profiles.Include(p => p.Posts).Include(p => p.Followers)
                .Include(p => p.Followings).ToListAsync();
        }

        public new async Task<Profile> Get(int id)
        {
            return await appContext.Profiles.Include(p => p.Posts).Include(p => p.Followers)
                .Include(p => p.Followings).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Profile GetByProfileName(string profileName)
        {
            return appContext.Profiles.Include(p => p.Posts).Include(p => p.Followers)
                .Include(p => p.Followings).FirstOrDefault(p => p.ProfileName == profileName);
        }

        public async Task<IEnumerable<Profile>> GetUserProfiles(int userId)
        {
            return await appContext.Profiles.Include(p => p.Posts).Include(p => p.Followers)
                .Include(p => p.Followings).Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
