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
    }
}
