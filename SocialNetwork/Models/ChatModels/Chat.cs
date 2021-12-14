using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.ChatModels
{
    public class Chat
    {
        public int Id { get; set; }
        public UserIdentity Me { get; set; }
        public UserIdentity With { get; set; }
        public List<Message> Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}
