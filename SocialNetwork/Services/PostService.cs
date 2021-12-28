using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public class PostService : IPostSevice
    {
        public async Task CommentAsync(SocialNetworkContext context, UserPost post, UserAccount account,string text)
        {
            Follower follower = new Follower
            {
                ImageUrl = account.UserInfo.ProfileImage,
                Username = account.UserIdentity.Username
            };
            Comment comment = new Comment
            {
                UserFrom = follower,
                Date = DateTime.Now,
                Text = text
            };
            post.Comments.Add(comment);
            context.UserPosts.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task LikeAsync(SocialNetworkContext context,UserPost post,UserAccount account)
        {
            bool alreadyLiked = post.Likes.Any(i=>i.From==account.UserIdentity.Username);
         
            if (alreadyLiked)
            {
                var likeToRemove = post.Likes.FirstOrDefault(i=>i.From==account.UserIdentity.Username);
                post.Likes.Remove(likeToRemove);
            }
            else
            {
                var like = new Like
                {
                    From = account.UserIdentity.Username,
                    FromPhoto = account.UserInfo.ProfileImage
                };
                post.Likes.Add(like);
            }
            context.UserPosts.Update(post);
            await context.SaveChangesAsync();
        }
    }
}
