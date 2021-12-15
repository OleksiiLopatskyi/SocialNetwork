using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class Comment
    {
        public int Id { get; set; }
        public UserIdentity UserFrom { get; set; }
        public string Date { get; set; }
        public List<Like> Likes { get; set; }
        public string Text { get; set; }
        public Comment()
        {
            Likes = new List<Like>();
        }
    }
}
