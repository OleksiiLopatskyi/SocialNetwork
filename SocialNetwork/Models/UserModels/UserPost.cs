using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class UserPost
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public List<ImagePost> Images { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }

        public UserPost()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
            Images = new List<ImagePost>();
        }

    }
}
