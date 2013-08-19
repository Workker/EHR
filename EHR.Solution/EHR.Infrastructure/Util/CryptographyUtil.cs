using System;
using System.Security.Cryptography;
using System.Text;

namespace EHR.Infrastructure.Util
{
    public static class CryptographyUtil
    {
        public static string EncryptToSha512(string text)
        {
            var uEncode = new UnicodeEncoding();
            var bytPassword = uEncode.GetBytes(text);
            var sha = new SHA512Managed();
            var hash = sha.ComputeHash(bytPassword);
            return Convert.ToBase64String(hash);
        }
    }
}