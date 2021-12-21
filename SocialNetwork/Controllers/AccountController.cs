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
                    return RedirectToAction("Index", "UserPage");
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
                _emailService.SendVerificationEmailAsync(_db, account.UserIdentity.Email, EmailAction.ConfirmEmail, Request);
                ViewBag.UserEmail = account.UserIdentity.Email;
                return View();
            }
            else
            {
                return RedirectToAction("Index","UserPage");
            }
            
        }
        [Route("[controller]/[action]/{code}")]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            var user = await _dbService.GetUserByUsername(_db,User.Identity.Name);
            if (code == user.UserIdentity.VerificationCode) {
                user.UserIdentity.isEmailConfirmed=EmailConfirm.Confirmed;
                user.UserIdentity.VerificationCode = string.Empty;
                _db.UserAccounts.Update(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            else return RedirectToAction("VerifyEmail");
        }
        [HttpGet]
        public IActionResult ForgotPasswordPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "UserPage");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>ForgotPasswordPage(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "UsePage");
            }
            var user = await _dbService.GetUserByEmail(_db,email);
            if (user != null)
            {
                _emailService.SendVerificationEmailAsync(_db,email,EmailAction.ResetPassword,Request);
                user.UserIdentity.ResetPasswordtStatus = ResetPasswordStatus.RequestForChange;
                _db.UserAccounts.Update(user);
                _db.SaveChanges();
                return Json(new { success = true, message = "Success! Please check email" });
            }
            else return Json(new { success = false, message = "There are no user with this email" });

        }
        [HttpGet]
        [Route("[controller]/[action]/{code?}/{email?}")]
        public async Task<IActionResult> ResetPassword(string code,string email)
        {
            UserAccount user = null;
            if (email!=null || code != null)
             user = await _dbService.GetUserByEmail(_db, email);
            else return RedirectToAction("Index", "UserPage");

            if (user.UserIdentity.ResetPasswordtStatus==ResetPasswordStatus.Default) return RedirectToAction("Index", "Home");
          
            if (code == user.UserIdentity.VerificationCode)
            {
                ViewBag.User = user.UserIdentity.Username;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","UserPage");
            }
            var user = await _dbService.GetUserByUsername(_db,model.Username);
            if (ModelState.IsValid)
            {
                user.UserIdentity.Password = model.NewPassword;
                user.UserIdentity.ResetPasswordtStatus = ResetPasswordStatus.Default;
                _db.UserAccounts.Update(user);
                _db.SaveChanges();
                return Json(new {success=true,message = "Changed"});
            }
            return Json(new {success=false,message="Incorrect link"});
        }

    }
}
