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
        public static async Task RemoveFromFollowing(this UserAccount account,SocialNetworkContext context,UserAccount userToRemove)
        {
            var followingUser = account.UserFollowing.FirstOrDefault(i=>i.Username==userToRemove.UserIdentity.Username);
            await Task.Run(() =>
            {
                account.UserFollowing.Remove(followingUser);
                context.Update(account);
                context.SaveChanges();
            });
           
        }
        public static async Task AddToFollowing(this UserAccount account, SocialNetworkContext context,UserAccount userToFollow)
        {
            var followingUser = new Follower {
                ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username 
            };
            await Task.Run(()=> {
                account.UserFollowing.Add(followingUser);
                context.Update(account);
                context.SaveChanges();
            });
           
        }
        public static async Task AddToFollowers(this UserAccount account, SocialNetworkContext context, UserAccount userToFollow)
        {
            var followingUser = new Follower
            {
                ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username
            };
            await Task.Run(()=> {
                account.UserFollowers.Add(followingUser);
                context.Update(account);
                context.SaveChanges();
            });
           
        }
        public static async Task RemoveFromFollowers(this UserAccount account, SocialNetworkContext context, UserAccount userToRemove)
        {
            var follower = account.UserFollowers.FirstOrDefault(i => i.Username == userToRemove.UserIdentity.Username);
            await Task.Run(() => {
                account.UserFollowers.Remove(follower);
                context.Update(account);
                context.SaveChanges();
            });

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
