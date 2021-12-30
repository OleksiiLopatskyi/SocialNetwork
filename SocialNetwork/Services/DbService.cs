using System;
using System.Collections.Generic;
using SocialNetwork.Models.Database;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Services
{
    public class DbService : IDbService
    {
        public async Task<UserAccount> GetUser(SocialNetworkContext context, LoginViewModel model)
        {
            var user = await context.UserAccounts.Include(i => i.UserIdentity).
                FirstOrDefaultAsync(i => (i.UserIdentity.Email == model.Login || i.UserIdentity.Username == model.Login) && i.UserIdentity.Password == model.Password);
            return user;
        }

        public async Task<UserAccount> GetUser(SocialNetworkContext context, RegisterViewModel model)
        {
            var user = await context.UserAccounts.Include(i => i.UserIdentity).
                FirstOrDefaultAsync(i => i.UserIdentity.Email == model.Email || i.UserIdentity.Username == model.Username);
            return user;
        }

        public async Task<UserAccount> GetUserByUsername(SocialNetworkContext context, string userName)
        {
            var userIdentity = await context.UserIdentities.FirstOrDefaultAsync(i => i.Username == userName);
            var userAccount = await context.UserAccounts
                .Include(i => i.UserChats)
                .Include(i => i.UserFollowing)
                .Include(i => i.UserFollowers)
                .Include(i => i.RecentlyUsers)
                .Include(i => i.UserInfo)
                .Include(i => i.UserPosts).ThenInclude(i=>i.Likes).Include(i=>i.UserPosts).ThenInclude(i=>i.Images)
                .FirstOrDefaultAsync(i => i.Id == userIdentity.Id);
            return userAccount;
        }

        public async Task<UserIdentity> GetUserIdentity(SocialNetworkContext context, UserAccount account)
        {
            var user = await context.UserIdentities.Include(i => i.Role).FirstOrDefaultAsync(i => i.Username == account.UserIdentity.Username);
            return user;
        }

        public async Task<UserAccount> RegisterUser(SocialNetworkContext context, RegisterViewModel model)
        {
            Role role = await context.Roles.FirstOrDefaultAsync(i => i.Name == "user");
            byte[]imageBytes;
            string imageUrl=string.Empty;
            if (model.ProfileImage != null)
            {
                 imageBytes = await model.ProfileImage.GetBytes();
                 imageUrl = imageBytes.GetImageFromByte();
            }
            else
            {
                imageUrl = "/Uploads/default-user.png";
            }
         

            UserIdentity userIdentity = new UserIdentity()
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                isEmailConfirmed = EmailConfirm.NotConfirmed,
                VerificationCode = string.Empty,
                Role = role,
            };

            UserInfo userInfo = new UserInfo()
            {
                ProfileImage = imageUrl,
                Age = DateTime.Now.Year - model.BirthDay.Year,
                Country = model.Country,
                City = model.City,
                Status = model.Status
            };
           
            UserAccount userAccount = new UserAccount()
            {
                UserIdentity = userIdentity,
                UserInfo = userInfo,
            };
            context.UserAccounts.Add(userAccount);
            context.SaveChanges();
            return userAccount;

        }
        public async Task<bool> CheckUserForEmailStatus(SocialNetworkContext context, UserAccount account)
        {
            var userAccount = await context.UserAccounts.Include(i => i.UserIdentity).FirstOrDefaultAsync(i => i.Id == account.Id);
            var userIdentity = userAccount.UserIdentity;
            if (userIdentity.isEmailConfirmed == EmailConfirm.Confirmed) return true;
            else return false;
        }
        public async Task<bool> isUserWithEmailExists(SocialNetworkContext context, RegisterViewModel model)
        {
            var user = await context.UserAccounts.Include(i => i.UserIdentity).
                FirstOrDefaultAsync(i => i.UserIdentity.Email == model.Email);
            if (user==null) return false;
            else return true;
        }

        public async Task<bool> isUserWithUsernameExists(SocialNetworkContext context, RegisterViewModel model)
        {
            var user = await context.UserAccounts.Include(i => i.UserIdentity).
               FirstOrDefaultAsync(i => i.UserIdentity.Username == model.Username);

            if (user==null) return false;
            else return true;
        }

        public async Task<UserAccount> GetUserByEmail(SocialNetworkContext context, string email)
        {
            var user =  await context.UserAccounts.Include(i => i.UserIdentity).FirstOrDefaultAsync(i=>i.UserIdentity.Email==email);
            return user;
        }

        public async Task<List<Follower>> GetFollowerList(SocialNetworkContext context, string username)
        {
            var user = await context.UserAccounts.Include(i=>i.UserFollowers).FirstOrDefaultAsync(i=>i.UserIdentity.Username==username);
            return user.UserFollowers;
        }

        public async Task<List<Follower>> GetFollowingList(SocialNetworkContext context, string username)
        {
            var user = await context.UserAccounts.Include(i => i.UserFollowing).FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            return user.UserFollowing;
        }

        public async Task<Follower> GetFollower(SocialNetworkContext context, string username,string followerName)
        {
            var user = await context.UserAccounts.Include(i=>i.UserFollowers).FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            var follower = user.UserFollowers.FirstOrDefault(i=>i.Username==followerName);
            return follower;
        }
        public async Task<bool> isFollowingAsync(SocialNetworkContext context, string username, string followerName)
        {
            var user = await context.UserAccounts.Include(i => i.UserFollowers).FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            var follower = user.UserFollowing.FirstOrDefault(i => i.Username == followerName);
            if (follower == null) return false;
            else return true;
        }
        public async Task<Follower> GetFollowing(SocialNetworkContext context, string username,string followerName)
        {
            var user = await context.UserAccounts.Include(i=>i.UserFollowing).FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            var follower = user.UserFollowing.FirstOrDefault(i => i.Username == followerName);
            return follower;
        }

        public  async Task  CreatePost(SocialNetworkContext context, UserPost post,UserAccount account)
        {
            account.UserPosts.Add(post);
            var posts =  await context.UserPosts.Include(i => i.Images).FirstOrDefaultAsync(i=>i.UserAccountId==account.Id&&i.Id==post.Id);
            context.Update(account);
            context.SaveChanges();
        }
        public async Task<List<UserPost>> GenerateFeeds(SocialNetworkContext context,string username)
        {
            var user = await GetUserByUsername(context,username);
            var FollowingUsersRecentlyPosts = new List<UserPost>();
            foreach (var item in user.UserFollowing)
            {
                var followingUserAccount = await GetUserByUsername(context, item.Username);
                foreach (var post in followingUserAccount.UserPosts.Where(i => (i.Date.Day - DateTime.Now.Day) <= 1))
                {
                    FollowingUsersRecentlyPosts.Add(post);
                }
            }
            
            return FollowingUsersRecentlyPosts;
        }

        public async Task<UserPost> GetPostById(SocialNetworkContext context,int id)
        {
            var post = await context.UserPosts.Include(i=>i.Comments).ThenInclude(i=>i.UserFrom).Include(i=>i.Likes).FirstOrDefaultAsync(i => i.Id == id);
            return post;
        }
    }
}
