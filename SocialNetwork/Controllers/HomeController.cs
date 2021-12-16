using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Models;
using SocialNetwork.Models.Database;
using SocialNetwork.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SocialNetworkContext _db;
        private IDbService _dbService;

        public HomeController(ILogger<HomeController> logger,IDbService service, SocialNetworkContext context)
        {
            _logger = logger;
            _dbService = service;
            _db = context;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            var userImageBase64 = Convert.ToBase64String(user.UserInfo.ProfileImage);
            ViewBag.Src_ProfileImage = string.Format($"data:image/jpg;base64,{userImageBase64}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
