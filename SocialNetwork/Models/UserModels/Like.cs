using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public class Like
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string FromPhoto { get; set; }
    }
}
