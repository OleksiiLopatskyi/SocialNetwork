using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class UserAccount
    {
        public int Id { get; set; }
        public UserIdentity UserIdentity { get; set; }
        public UserInfo UserInfo { get; set; }
        public byte[] ProfileImage { get; set; }
        public List<UserPost> UserPosts { get; set; }
        public List<UserIdentity> UserFriends { get; set; }
        public List<UserAccount> FriendRequests { get; set; }
        public UserAccount()
        {
            UserPosts = new List<UserPost>();
            UserFriends = new List<UserIdentity>();
            FriendRequests = new List<UserAccount>();
        }
    }
}
