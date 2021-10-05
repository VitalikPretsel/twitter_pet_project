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
        Task<IEnumerable<int?>> GetProfileFollowings(int profileId);
        Task<Following> Get(int followerId, int followingId);
        Task<bool> Any(int followerId, int followingId);
    }
}
