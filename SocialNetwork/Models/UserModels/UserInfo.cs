using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
   public enum UserStatus{
        Free,
        Relations,
        Married,
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public byte[] ProfileImage { get; set; }
        public UserStatus Status { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
