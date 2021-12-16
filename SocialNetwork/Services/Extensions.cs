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
        public static string GetImageFromByte(this byte[] byteString)
        {
            using (var memoryStream = new MemoryStream(byteString))
            {
                var userImageBase64 = Convert.ToBase64String(byteString);
                string image = string.Format($"data:image/jpg;base64,{userImageBase64}");
                return image;
            }
        }
    }
}
