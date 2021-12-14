using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class UserPost
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<UserIdentity> Likes { get; set; }
        public List<PostComment> Comments { get; set; }

        public UserPost()
        {
            Likes = new List<UserIdentity>();
            Comments = new List<PostComment>();
        }

    }
}
