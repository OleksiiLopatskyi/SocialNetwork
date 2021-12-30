using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public interface IPostSevice
    {
        Task LikeAsync(SocialNetworkContext context,UserPost post,UserAccount account);
        Task CommentAsync(SocialNetworkContext context,UserPost post,UserAccount account,string text);
    }
}
