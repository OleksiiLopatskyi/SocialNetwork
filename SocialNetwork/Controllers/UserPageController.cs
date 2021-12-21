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
            var users = await _db.UserAccounts.Include(i=>i.UserInfo).Include(i => i.UserIdentity).Where(i=>i.UserIdentity.Username.Contains(value)).ToListAsync();
            return Json(users);
        }
        [Route("[controller]/[action]/{name}")]
        public async Task<IActionResult> Profile(string name)
        {
            var logedInUser = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var user = await _dbService.GetUserByUsername(_db,name);
            var model = (logedInUser,user);
            return View(model);
        }
        public async Task<IActionResult> Follow(string username)
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var userToFollow = await _dbService.GetUserByUsername(_db,username);

            var following = new Follower { ImageUrl = userToFollow.UserInfo.ProfileImage,
                Username = userToFollow.UserIdentity.Username };
            var follower = new Follower { ImageUrl = user.UserInfo.ProfileImage,
                Username = user.UserIdentity.Username };
            user.UserFollowing.Add(following);
            userToFollow.UserFollowers.Add(follower);
            _db.UserAccounts.Update(user);
            _db.UserAccounts.Update(userToFollow);
            _db.SaveChanges();
            return Json(new {success=true,Follow=true});
        }
        public async Task<IActionResult> UnFollow(string username)
        {
            var user = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            var userToFollow = await _dbService.GetUserByUsername(_db, username);
            var followwing = await _dbService.GetFollower(_db,userToFollow.UserIdentity.Username);
            var follower = await _dbService.GetFollower(_db, user.UserIdentity.Username);

            user.UserFollowing.Remove(followwing);
            userToFollow.UserFollowers.Remove(follower);
            _db.UserAccounts.Update(user);
            _db.SaveChanges();
            _db.UserAccounts.Update(userToFollow);
            _db.SaveChanges();
            return Json(new { success = true, Follow = false });
        }
        public async Task<IActionResult> GetUserInfo(string username)
        {
            var logedInUser = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            bool isFollowing = logedInUser.UserFollowing.Any(i=>i.Username==username);

            return Json(new {follow=isFollowing});
        }
        public async Task<IActionResult> Followers(string username)
        {
            var followers = await _dbService.GetFollowers(_db,username);
            return Json(followers);
        }
        public async Task<IActionResult> Following(string username)
        {
            var followers = await _dbService.GetFollowers(_db, username);
            return Json(followers);
        }
    }
}
