using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DataContext
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Reply> Replies { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<Following> Followings { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
        DbSet<T> Set<T>() where T : class;
    }
}
