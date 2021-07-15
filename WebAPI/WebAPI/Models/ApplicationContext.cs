using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Following>()
        //        .HasOne(f => f.FollowerProfile)
        //        .WithMany()
        //        .HasForeignKey(f => f.FollowerProfileId)
        //        .OnDelete(DeleteBehavior.NoAction);
        //    modelBuilder.Entity<Following>()
        //        .HasOne(f => f.FollowingProfile)
        //        .WithMany()
        //        .HasForeignKey(f => f.FollowingProfileId)
        //        .OnDelete(DeleteBehavior.NoAction);
        //}
    }
}
