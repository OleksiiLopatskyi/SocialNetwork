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
                .Include(i=>i.UserFollowers)
                .Include(i => i.UserInfo)
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
            var profileImage = await model.ProfileImage.GetBytes();

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
                ProfileImage = profileImage.GetImageFromByte(),
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

        public async Task<List<Follower>> GetFollowers(SocialNetworkContext context, string username)
        {
            var user = await context.UserAccounts.FirstOrDefaultAsync(i=>i.UserIdentity.Username==username);
            return user.UserFollowing;
        }

        public async Task<List<Follower>> GetFollowing(SocialNetworkContext context, string username)
        {
            var user = await context.UserAccounts.FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            return user.UserFollowing;
        }

        public async Task<Follower> GetFollower(SocialNetworkContext context, string username)
        {
            var user = await context.UserAccounts.FirstOrDefaultAsync(i => i.UserIdentity.Username == username);
            var follower = user.UserFollowers.FirstOrDefault(i=>i.Username==username);
            return follower;
        }
    }
}
