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
    }
}
