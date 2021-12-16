using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services
{
    public static class Extensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public static IFormFile GetIFormFileFromByte(this byte[] byteString)
        {
            using (var memoryStream = new MemoryStream(byteString))
            {
                IFormFile file = new FormFile(memoryStream,0,byteString.Length,"Image","UserProfileImage");
                return file;
            }
        }
    }
}
