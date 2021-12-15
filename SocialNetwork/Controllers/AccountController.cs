using Microsoft.AspNetCore.Mvc;
using System;
using SocialNetwork.ViewModels;
using System.Collections.Generic;
using SocialNetwork.Models.Database;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Services;
using SocialNetwork.Models.UserModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private SocialNetworkContext _db;
        private IDbService _dbService;
        public AccountController(SocialNetworkContext shoppingContext,IDbService db_service)
        {
            _db = shoppingContext;
            _dbService=db_service;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userAccount =  await _dbService.GetUser(_db,model);
                if (userAccount != null)
                {
                    var userIdentity = await _dbService.GetUserIdentity(_db, userAccount);
                    await Authenticate(userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login or(and) password is(are) incorrect");
            }
            return View(model);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbService.GetUser(_db,model).Result;
                if (user == null)
                {
                    await _dbService.RegisterUser(_db,model);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Incorrect login and(or)password");
            }
            return View(model);
        }
        private async Task Authenticate(UserIdentity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                                                   ClaimsIdentity.DefaultNameClaimType,
                                                   ClaimsIdentity.DefaultRoleClaimType
                                                   );
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    
    }
}
