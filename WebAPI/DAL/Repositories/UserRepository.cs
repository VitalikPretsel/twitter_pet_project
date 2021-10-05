using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public User FindUserByName(string userName)
        {
            return appContext.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User FindUserByEmail(string userEmail)
        {
            return appContext.Users.FirstOrDefault(u => u.Email == userEmail);
        }
    }
}
