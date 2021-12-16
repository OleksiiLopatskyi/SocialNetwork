using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Models.UserModels;

namespace SocialNetwork.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        //Identity Details
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        //Profile Details
        public IFormFile ProfileImage { get; set; }
        public DateTime BirthDay { get; set; }
        public UserStatus Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
