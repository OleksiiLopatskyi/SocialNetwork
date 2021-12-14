using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.ChatModels;
using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.Database
{
    public class SocialNetworkContext:DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<PostComment> UserPostComments { get; set; }
        public DbSet<Chat> UserChats { get; set; }
        public DbSet<Message> UserMessages { get; set; }
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext>options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
