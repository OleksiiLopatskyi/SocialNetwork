using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.UserModels
{
    public enum EmailConfirm
    {
        Confirmed,
        NotConfirmed
    }
    public class UserIdentity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public EmailConfirm isEmailConfirmed { get; set; }
        public string VerificationCode { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
