using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public static class UserAccountExtensions
    {
        public static void RemoveFromFollowing(this UserAccount account,SocialNetworkContext context,UserAccount userToRemove)
        {
            var followingUser = account.UserFollowing.FirstOrDefault(i=>i.Username==userToRemove.UserIdentity.Username);
            account.UserFollowing.Remove(followingUser);
            context.Update(account);
            context.SaveChanges();
        }
        public static void AddToFollowing(this UserAccount account, SocialNetworkContext context,UserAccount userToFollow)
        {
            var followingUser = new Follower {
                ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username 
            };
            account.UserFollowing.Add(followingUser);
            context.Update(account);
            context.SaveChanges();
        }
        public static void AddToFollowers(this UserAccount account, SocialNetworkContext context, UserAccount userToFollow)
        {
            var followingUser = new Follower
            {
                ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username
            };
            account.UserFollowers.Add(followingUser);
            context.Update(account);
            context.SaveChanges();
        }
        public static void RemoveFromFollowers(this UserAccount account, SocialNetworkContext context, UserAccount userToRemove)
        {
            var follower= account.UserFollowers.FirstOrDefault(i => i.Username == userToRemove.UserIdentity.Username);
            account.UserFollowers.Remove(follower);
            context.Update(account);
            context.SaveChanges();
        }
        public static void AddRecentlyUser(this UserAccount account, SocialNetworkContext context, UserAccount userToFollow)
        {
            var followingUser = new Follower
            {
                ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username
            };
            
            if (account.RecentlyUsers.Count() >= 4)
            {
                if(followingUser.Username != account.UserIdentity.Username)
                {
                    if (account.RecentlyUsers.Any(i => i.Username == followingUser.Username))
                    {
                        account.RecentlyUsers.RemoveAll(i=>i.Username==followingUser.Username);
                        account.RecentlyUsers.Add(followingUser);
                    }
                    else
                    {
                        account.RecentlyUsers.Add(followingUser);
                    }
                }                
            }
            else
            {
                if (followingUser.Username != account.UserIdentity.Username)
                {
                    if (account.RecentlyUsers.Any(i => i.Username == followingUser.Username))
                    {
                        account.RecentlyUsers.RemoveAll(i => i.Username == followingUser.Username);
                        account.RecentlyUsers.Add(followingUser);
                    }
                    else
                    {
                        account.RecentlyUsers.Add(followingUser);
                    }
                }
            }
            context.Update(account);
            context.SaveChanges();
        }
    }
}
