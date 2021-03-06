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
        Task<UserAccount> GetUserByEmail(SocialNetworkContext context, string email);
        Task<UserAccount> GetUserByUsername(SocialNetworkContext context,string userName);
        Task<UserAccount> RegisterUser(SocialNetworkContext context,RegisterViewModel model);
        Task<UserIdentity> GetUserIdentity(SocialNetworkContext context, UserAccount account);
        Task<bool> CheckUserForEmailStatus(SocialNetworkContext context, UserAccount account);
        Task<bool> isUserWithEmailExists(SocialNetworkContext context, RegisterViewModel model);
        Task<bool> isUserWithUsernameExists(SocialNetworkContext context, RegisterViewModel model);
        Task<Follower> GetFollower(SocialNetworkContext context, string username, string followerName);
        Task<List<Follower>> GetFollowerList(SocialNetworkContext context, string username);
        Task<List<Follower>> GetFollowingList(SocialNetworkContext context, string username);
        Task<Follower> GetFollowing(SocialNetworkContext context, string username, string followerName);
        Task<bool> isFollowingAsync(SocialNetworkContext context, string username, string followerName);
        Task CreatePost(SocialNetworkContext context, UserPost post,UserAccount account);
        Task<List<UserPost>> GenerateFeeds(SocialNetworkContext context, string username);
        Task<UserPost> GetPostById(SocialNetworkContext context,int id);

       
    }
}
