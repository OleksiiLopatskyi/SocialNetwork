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

        public async Task<UserIdentity> GetUserIdentity(SocialNetworkContext context, UserAccount account)
        {
            var user = await context.UserIdentities.Include(i => i.Role).FirstOrDefaultAsync(i=>i.Id==account.Id);
            return user;
        }

        public async Task<UserAccount> RegisterUser(SocialNetworkContext context, RegisterViewModel model)
        {
            Role role = await context.Roles.FirstOrDefaultAsync(i => i.Name == "user");
            UserIdentity userIdentity = new UserIdentity()
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                Role = role,
            };
            UserInfo userInfo = new UserInfo()
            {
                ProfileImage = await model.ProfileImage.GetBytes(),
                Age = model.Age,
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
    }
}
