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
    [Authorize(Policy = "UserWithConfirmedEmailOnly")]
    public class UserPageController : Controller
    {
        private SocialNetworkContext _db;
        private IDbService _dbService;
        public UserPageController(SocialNetworkContext context,IDbService dbService)
        {
            _db = context;
            _dbService = dbService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            return View(user);
        }
        public async Task<IActionResult> Search(string value)
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var users = await _db.UserAccounts.Include(i=>i.UserInfo).Include(i => i.UserIdentity).Where(i=>i.UserIdentity.Username.Contains(value)).ToListAsync();
           
            return Json(new {foundUsers = users,recentlyUsers=user.RecentlyUsers.OrderByDescending(i=>i.Id)});
        }
        [Route("[controller]/[action]/{name}")]
        public async Task<IActionResult> Profile(string name)
        {
            var logedInUser = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var user = await _dbService.GetUserByUsername(_db,name);
            logedInUser.AddRecentlyUser(_db,user);
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
    }
}
