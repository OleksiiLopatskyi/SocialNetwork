using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.ChatModels
{
    public class Message
    {
        public int Id { get; set; }
        public string UserFrom { get; set; }
        public string UserTo { get; set; }
        public List<Like> Likes { get; set; }
        public string Text { get; set; }
        public Message()
        {
            Likes = new List<Like>();
        }
    }
}
