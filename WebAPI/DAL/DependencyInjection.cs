using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoriesWithDbContext(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReplyRepository, ReplyRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IFollowingRepository, FollowingRepository>();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TwitterDB;Trusted_Connection=True;MultipleActiveResultSets=True"));

            return services;
        }
    }
}
