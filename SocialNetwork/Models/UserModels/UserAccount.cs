using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Models.ChatModels;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class UserAccount
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public int UserIdentityId { get; set; }
        public int UserInfoId { get; set; }
        public List<UserPost> UserPosts { get; set; }
        public List<Follower> UserFollowers { get; set; }
        public List<Follower> UserFollowing { get; set; }
        public List<UserInfo> FriendRequests { get; set; }
        public List<Chat> UserChats { get; set; }
        public UserIdentity UserIdentity { get; set; }
        public UserInfo UserInfo { get; set; }
        public UserAccount()
        {
            UserPosts = new List<UserPost>();
            UserFollowers = new List<Follower>();
            UserFollowing = new List<Follower>();
            FriendRequests = new List<UserInfo>();
            UserChats = new List<Chat>();
        }
    }
}
