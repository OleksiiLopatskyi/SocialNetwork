using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.ViewModels
{
    public class IndexViewModel
    {
        public UserAccount Profile { get; set; }
        public List<UserAccount> Stories { get; set; }
        public List<UserPost> Posts { get; set; }
    }
}
