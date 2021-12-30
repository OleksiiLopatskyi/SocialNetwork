using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.ViewModels
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Photos { get; set; }
    }
}
