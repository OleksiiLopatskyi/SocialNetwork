using Microsoft.AspNetCore.Http;
using SocialNetwork.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public interface IEmailService
    {
        void SendVerificationEmailAsync(SocialNetworkContext context,string emailID, string emailFor, HttpRequest request);
    }
}
