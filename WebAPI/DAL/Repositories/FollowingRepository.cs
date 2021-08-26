using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class FollowingRepository : GenericRepository<Following>, IFollowingRepository
    {
        public FollowingRepository(ApplicationContext context) : base(context)
        {

        }

        public int GetProfileFollowersAmount(int profileId)
        {
            return appContext.Followings.Where(f => f.FollowingProfileId == profileId).Count();
        }

        public int GetProfileFollowingsAmount(int profileId)
        {
            return appContext.Followings.Where(f => f.FollowerProfileId == profileId).Count();
        }
    }
}
