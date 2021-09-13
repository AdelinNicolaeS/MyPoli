using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace MyPoli.Common
{
    public static class Utils
    {
        public static readonly int PageSize = 10;
        public static readonly string Unauthorized = "Error_Unauthorized";
        public static readonly string NotFound = "Error_NotFound";

        public static byte[] FileToByteArray(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            var sOutput = new StringBuilder(arrInput.Length);
            for (var i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public static string MyHashFunction(string input)
        {
            var sha = SHA256.Create();
            var aux = Encoding.ASCII.GetBytes(input);
            var result = sha.ComputeHash(aux);
            return ByteArrayToString(result);
        }

    }
}
