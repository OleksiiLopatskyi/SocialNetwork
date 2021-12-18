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
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authentication;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Policy;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private SocialNetworkContext _db;
        private IDbService _dbService;
        private IEmailService _emailService;
        public AccountController(SocialNetworkContext shoppingContext,IDbService db_service,IEmailService emailService)
        {
            _db = shoppingContext;
            _dbService=db_service;
            _emailService = emailService;
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userAccount =  await _dbService.GetUser(_db,model);
                if (userAccount != null)
                {
                    var userIdentity = await _dbService.GetUserIdentity(_db, userAccount);
                    await Authenticate(userIdentity);
                   
                   return Json(new { success = true });
                }
            }
            return Json(new {success=false,errorText = "Login or(and) password is(are) incorrect" });
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
                var user = await _dbService.GetUser(_db,model);
                if (user != null)
                {
                    bool userWithEmailExists = await _dbService.isUserWithEmailExists(_db, model);
                    bool userWithUsernameExists = await _dbService.isUserWithUsernameExists(_db, model);
                    if (userWithEmailExists && userWithUsernameExists)
                    {
                        ModelState.AddModelError("", "User with current email or username already exists");
                    }
                    else
                    {
                        if (userWithEmailExists)
                        {
                            ModelState.AddModelError("", "User with current email already exists");
                        }
                        if (userWithUsernameExists)
                        {
                            ModelState.AddModelError("", "User with current username already exists");
                        }
                    }
                    
                }
                else
                {
                    var registeredUser = await _dbService.RegisterUser(_db, model);
                    await Authenticate(registeredUser.UserIdentity);
                    return RedirectToAction("Index", "Home");
                }


            }
            return View(model);
        }
        private async Task Authenticate(UserIdentity user)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                    new Claim("EmailStatus",user.isEmailConfirmed.ToString())
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
        public async Task<IActionResult> VerifyEmail()
        {
            var account = await _dbService.GetUserByUsername(_db, User.Identity.Name);
            bool isUserConfirmed = await _dbService.CheckUserForEmailStatus(_db,account);
            if (!isUserConfirmed)
            {
                _emailService.SendVerificationEmailAsync(_db, account.UserIdentity.Email, "VerifyEmailLink", Request);
                ViewBag.UserEmail = account.UserIdentity.Email;
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
            
        }
        [Route("[controller]/[action]/{code}")]
        public async Task<IActionResult> VerifyEmailLink(string code)
        {
            var user = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            if (code == user.UserIdentity.EmailVerificationCode) {
                user.UserIdentity.isEmailConfirmed=EmailConfirm.Confirmed;
                user.UserIdentity.EmailVerificationCode = string.Empty;
                _db.UserAccounts.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            else return RedirectToAction("VerifyEmail");
        }
    }
}
