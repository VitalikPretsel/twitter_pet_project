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
    public class FollowingRepository : GenericRepository<Following>, IFollowingRepository
    {
        public FollowingRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<IEnumerable<int?>> GetProfileFollowings(int profileId)
        {
            return await appContext.Followings.Where(f => f.FollowerProfileId == profileId)
                .Select(f => f.FollowingProfileId).ToListAsync();
        }
    }
}
