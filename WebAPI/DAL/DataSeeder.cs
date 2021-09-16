using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL
{
    static class DataSeeder
    {
        static public void SeedDataBase(ModelBuilder builder)
        {
            builder.Entity<User>().HasData( // password = "testpass" for all users
                new User { Id = 1, UserName = "TestUser1", Email = "user_1@testmail.com", PasswordHash = "iV44iFF1LTb/t5VpyTwDobB8hQZ7dKRzgH43Tjj/yWc=", PasswordSalt = new byte[] { 45, 202, 189, 71, 44, 206, 203, 143, 216, 27, 78, 72, 212, 121, 141, 214 } },
                new User { Id = 2, UserName = "TestUser2", Email = "user_2@testmail.com", PasswordHash = "iV44iFF1LTb/t5VpyTwDobB8hQZ7dKRzgH43Tjj/yWc=", PasswordSalt = new byte[] { 45, 202, 189, 71, 44, 206, 203, 143, 216, 27, 78, 72, 212, 121, 141, 214 } },
                new User { Id = 3, UserName = "TestUser3", Email = "user_3@testmail.com", PasswordHash = "iV44iFF1LTb/t5VpyTwDobB8hQZ7dKRzgH43Tjj/yWc=", PasswordSalt = new byte[] { 45, 202, 189, 71, 44, 206, 203, 143, 216, 27, 78, 72, 212, 121, 141, 214 } });

            builder.Entity<Profile>().HasData(
                new Profile { Id = 1, ProfileName = "Tester1_profile1", ProfileDescription = "I am TestUser1 and it's my 1 of 2 profiles!", UserId = 1 },
                new Profile { Id = 2, ProfileName = "Tester1_profile2", ProfileDescription = "I am TestUser1 and it's my 2 of 2 profiles!", UserId = 1 },
                new Profile { Id = 3, ProfileName = "Tester2_profile1", ProfileDescription = "I am TestUser2 and it's my only profile!", UserId = 2 },
                new Profile { Id = 4, ProfileName = "Tester3_profile1", ProfileDescription = "I am TestUser3, it's my only profile and I have no posts!", UserId = 3 }
                );

            builder.Entity<Following>().HasData(
                new Following { Id = 1, FollowerProfileId = 1, FollowingProfileId = 2 },
                new Following { Id = 2, FollowerProfileId = 1, FollowingProfileId = 3 },
                new Following { Id = 3, FollowerProfileId = 2, FollowingProfileId = 1 },
                new Following { Id = 4, FollowerProfileId = 3, FollowingProfileId = 1 },
                new Following { Id = 5, FollowerProfileId = 3, FollowingProfileId = 2 });

            List<Post> posts = new List<Post>();

            for (int i = 1; i <= 100; i++)
            {
                posts.Add(new Post { Id = i, PostText = "Post " + (1 + i % 30 - i % 3) + " of profile " + (1 + i % 3) + ".", PostingDate = new DateTime(2021, 9, 15, i % 24, i % 60, 0), ProfileId = 1 + i % 3 });
            }

            builder.Entity<Post>().HasData(posts);

            List<Reply> replies = new List<Reply>();

            for (int i = 1; i <= 100; i++)
            {
                replies.Add(new Reply { Id = i, ReplyText = "Reply of profile " + (1 + i % 2) + ".", ReplyingDate = new DateTime(2021, 8, 15, 2 + i % 20, i % 60, 0), PostId = 1 + i % 50, ProfileId = 1 + i % 2 });
            }
            for (int i = 101; i <= 200; i++)
            {
                replies.Add(new Reply { Id = i, ReplyText = "Reply of profile " + (1 + i % 4) + ".", ReplyingDate = new DateTime(2021, 8, 15, 3 + i % 20, i % 60, 0), PostId = 1 + (i * 2) % 100, ProfileId = 1 + i % 4 });
            }

            builder.Entity<Reply>().HasData(replies);

            List<Like> likes = new List<Like>();

            for (int i = 1; i <= 100; i++)
            {
                likes.Add(new Like { Id = i, PostId = i, ProfileId = 1 });
            }
            for (int i = 1; i <= 50; i++)
            {
                likes.Add(new Like { Id = 100 + i, PostId = i * 2, ProfileId = 2 });
            }
            for (int i = 1; i <= 33; i++)
            {
                likes.Add(new Like { Id = 150 + i, PostId = i * 3, ProfileId = 3 });
            }
            for (int i = 1; i <= 25; i++)
            {
                likes.Add(new Like { Id = 183 + i, PostId = i * 4, ProfileId = 4 });
            }

            builder.Entity<Like>().HasData(likes);
        }
    }
}
