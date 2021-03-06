using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.DataContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Following>()
                .HasOne(f => f.FollowerProfile).WithMany(p => p.Followings);
            builder.Entity<Following>()
                .HasOne(f => f.FollowingProfile).WithMany(p => p.Followers);

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<Profile>()
                .HasIndex(p => p.ProfileName)
                .IsUnique();

            DataSeeder.SeedDataBase(builder);
        }
    }
}
