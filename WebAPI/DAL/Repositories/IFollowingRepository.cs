using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IFollowingRepository : IGenericRepository<Following>
    {
        int GetProfileFollowersAmount(int profileId);
        int GetProfileFollowingsAmount(int profileId);
    }
}
