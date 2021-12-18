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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<Comment> UserPostComments { get; set; }
        public DbSet<Chat> UserChats { get; set; }
        public DbSet<Message> UserMessages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext>options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Admin Identity
            string adminEmail = "admin@gmail.com";
            string adminUsername = "admin";
            string adminPassword = "12345";

            string adminRoleName = "admin";
            string userRoleName = "user";

            //Admin Info
            int adminAge = 18;
            string adminCountry = "Ukraine";
            string adminCity = "Lviv";
            UserStatus adminStatus = UserStatus.Free;

            Role adminRole = new Role() {Id=1,Name=adminRoleName};
            Role userRole = new Role() {Id=2,Name=userRoleName};

            UserIdentity adminIdentity = new UserIdentity() {Id=1,Email = adminEmail, RoleId=2,isEmailConfirmed=EmailConfirm.Confirmed,Username = adminUsername, Password = adminPassword};
            UserInfo adminInfo = new UserInfo() {Id=1,Age = adminAge, City = adminCity, Country = adminCountry, Status = adminStatus };
            UserAccount admin = new UserAccount() {Id=1,UserIdentityId=1,UserInfoId=1};
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<UserIdentity>().HasData(new UserIdentity[] { adminIdentity });
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo[] {adminInfo});
            modelBuilder.Entity<UserAccount>().HasData(new UserAccount[] {admin});

            base.OnModelCreating(modelBuilder);
        }
    }
}
