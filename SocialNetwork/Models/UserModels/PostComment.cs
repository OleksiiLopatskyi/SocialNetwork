using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class PostComment
    {
        public int Id { get; set; }
        public UserIdentity UserFrom { get; set; }
        public string Date { get; set; }
        public List<UserIdentity> Likes { get; set; }
        public string Text { get; set; }
        public PostComment()
        {
            Likes = new List<UserIdentity>();
        }
    }
}
