using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using SocialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class PostActionController : Controller
    {
        private SocialNetworkContext _db;
        private IDbService _dbService;
        private IPostSevice _postSevice;
        public PostActionController(SocialNetworkContext context,IDbService service,IPostSevice postService)
        {
            _db = context;
            _dbService = service;
            _postSevice = postService;
        }
        public async Task<IActionResult> Like(int postId)
        {
            var user = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var post = await _dbService.GetPostById(_db,postId);
            await _postSevice.LikeAsync(_db,post,user);
            return Json(new {count=post.Likes.Count(),Liked=post.Likes.Any(i=>i.From==user.UserIdentity.Username)});
        }
        public async Task<IActionResult> Comment(string text,int postId)
        {
            var user = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var post = await _dbService.GetPostById(_db,postId);
            await _postSevice.CommentAsync(_db,post,user,text);
            return Json(new {success=true});
        }
        public async Task<IActionResult> ShowComments(int postId)
        {
            var post = await _dbService.GetPostById(_db,postId);
            return Json(new {comments=post.Comments});
        }
    }
}
