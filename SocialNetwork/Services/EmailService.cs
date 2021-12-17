using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using SocialNetwork.Models.Database;

namespace SocialNetwork.Services
{
    public class EmailService : IEmailService
    {
        public void SendVerificationEmailAsync(SocialNetworkContext context,string emailID, string emailFor, HttpRequest request)
            {
            string activationCode = Guid.NewGuid().ToString();
            var user = context.UserIdentities.FirstOrDefault(i=>i.Email==emailID);
            user.EmailVerificationCode = activationCode;
            context.UserIdentities.Update(user);
            context.SaveChanges();
            var verifyUrl = "/Account/" + emailFor + "/" + activationCode;
            var link = UriHelper.GetDisplayUrl(request).Replace(UriHelper.GetEncodedPathAndQuery(request), verifyUrl);

            var fromEmail = new MailAddress("AdminSocialNetwork@gmail.com", "Admin");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Oleksiy.lopatskiy2003"; // Replace with actual password


            string subject = "Confirm Email";
            string body = "";
            if (emailFor == "VerifyEmailLink")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }

            var smtp = new SmtpClient
            {
                Host= "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("sociallviv.ad@gmail.com", fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
             smtp.Send(message);
        }
    }
}
