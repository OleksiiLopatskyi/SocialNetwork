using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
