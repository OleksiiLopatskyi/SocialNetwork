using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public interface IDbService
    {
        Task<UserAccount> GetUser(SocialNetworkContext context,LoginViewModel model);
        Task<UserAccount> GetUser(SocialNetworkContext context, RegisterViewModel model);
        Task<UserAccount> GetUserByUsername(SocialNetworkContext context,string userName);
        Task<UserAccount> RegisterUser(SocialNetworkContext context,RegisterViewModel model);
        Task<UserIdentity> GetUserIdentity(SocialNetworkContext context, UserAccount account);

    }
}
