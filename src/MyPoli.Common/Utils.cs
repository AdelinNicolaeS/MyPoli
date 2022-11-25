using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MyPoli.Common
{
    public static class Utils
    {
        public static readonly int PageSize = 10;
        public static readonly string Unauthorized = "Error_Unauthorized";
        public static readonly string NotFound = "Error_NotFound";
        public static readonly char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
        public static readonly int MinimumDiff_EditDist = 3;

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

        private static int min(int x, int y, int z)
        {
            if (x <= y && x <= z)
                return x;
            if (y <= x && y <= z)
                return y;
            return z;
        }

        public static int EditDist(String str1, String str2)
        {
            return EditDistAux(str1, str2, str1.Length, str2.Length);
        }

        private static int EditDistAux(String str1, String str2, int m,
                            int n)
        {
            if (m == 0)
                return n;

            if (n == 0)
                return m;

            if (str1[m - 1] == str2[n - 1])
                return EditDistAux(str1, str2, m - 1, n - 1);

            return 1
                + min(EditDistAux(str1, str2, m, n - 1), // Insert
                      EditDistAux(str1, str2, m - 1, n), // Remove
                      EditDistAux(str1, str2, m - 1,
                               n - 1) // Replace
                  );
        }

        public static List<string> ConvertArrayToList(string[] array)
        {
            List<string> lst = new();
            lst.AddRange(array);
            return lst;
        }

    }
}
