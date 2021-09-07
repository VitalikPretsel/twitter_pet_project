using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
     
        public User FindUserByLoginModel(LoginModel loginModel)
        {
            return appContext.Users.FirstOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);
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
