using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Database;
using SocialNetwork.Models.UserModels;
using SocialNetwork.Services;
using SocialNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize(Policy = "UserWithConfirmedEmailOnly")]
    public class UserPageController : Controller
    {
        private SocialNetworkContext _db;
        private IDbService _dbService;
        private readonly IWebHostEnvironment environment;
        public UserPageController(SocialNetworkContext context,IDbService dbService,IWebHostEnvironment env)
        {
            _db = context;
            _dbService = dbService;
            environment = env;
        }
        //Index
        public async Task<IActionResult> Index()
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var posts = await _dbService.GenerateFeeds(_db, user.UserIdentity.Username);
            var model = new IndexViewModel
            {
                Profile = user,
                Posts = posts.OrderByDescending(i=>i.Date).ToList()
            };
            return View(model);
        }

        //Search
        public async Task<IActionResult> Search(string value)
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var users = await _db.UserAccounts.Include(i=>i.UserInfo).Include(i => i.UserIdentity).Where(i=>i.UserIdentity.Username.Contains(value)).ToListAsync();
            return Json(new {foundUsers = users,recentlyUsers=user.RecentlyUsers.OrderByDescending(i=>i.Id)});
        }

        //Profile
        [Route("[controller]/[action]/{name}")]
        public async Task<IActionResult> Profile(string name)
        {
            var logedInUser = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var user = await _dbService.GetUserByUsername(_db,name);
            logedInUser.AddRecentlyUser(_db,user);
            List<UserPost> reversedPosts = user.UserPosts.OrderByDescending(i=>i.Id).ToList();
            user.UserPosts = reversedPosts;
            var model = (logedInUser,user);
            return View(model);
        }

        public async Task<IActionResult> Follow(string username)
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var userToFollow = await _dbService.GetUserByUsername(_db,username);
            var isFollowing = await _dbService.isFollowingAsync(_db,user.UserIdentity.Username,username);
            if (isFollowing)
            {
               await user.RemoveFromFollowing(_db,userToFollow);
               await userToFollow .RemoveFromFollowers(_db, user);
               return Json(new { success = true, Follow = false, followers = userToFollow.UserFollowers.Count() });
            }
            else
            {
                await user.AddToFollowing(_db, userToFollow);
                await userToFollow .AddToFollowers(_db, user);
                return Json(new { success = true, Follow = true, followers = userToFollow.UserFollowers.Count()});
            }
        }

        public async Task<IActionResult> GetUserInfo(string username)
        {
            var logedInUser = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            bool isFollowing = logedInUser.UserFollowing.Any(i=>i.Username==username);

            return Json(new {follow=isFollowing});
        }
        public async Task<IActionResult> Followers(string username)
        {
            var followers = await _dbService.GetFollowerList(_db,username);
            return Json(followers);
        }

        public async Task<IActionResult> Following(string username)
        {
            var followers = await _dbService.GetFollowingList(_db, username);
            return Json(followers);
        }
        public async Task<IActionResult> CreatePost()
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var userName = user.UserIdentity.Username;
           
            var pathImage =string.Empty;

            var postInfo = HttpContext.Request.Form;
            var postTitle = postInfo["Title"].First();
            var postDescription = postInfo["Description"].First();
            IFormFileCollection files = postInfo.Files;
            var pathFolder = $@"{environment.WebRootPath}\Uploads\Posts\{userName}\{postTitle}";
            bool isPathExists = Directory.Exists(pathFolder);
            if (!isPathExists)
            {
                Directory.CreateDirectory(pathFolder);
            }
            UserPost post = new UserPost()
            {
                Title = postTitle,
                Description = postDescription,
                Date = DateTime.Now,
            };

            foreach (var item in files)
            {
                pathImage = $@"{pathFolder}\{item.FileName}";
                ImagePost image = new ImagePost
                {
                    Url = $"/Uploads/Posts/{userName}/{post.Title}/{item.FileName}"
                };
                post.Images.Add(image);
                using (Stream fileStream = new FileStream(pathImage, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }
            }
            await _dbService.CreatePost(_db,post,user);

            return Json(new {success=true});
        }
       
    }
}
