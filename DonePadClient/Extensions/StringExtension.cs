using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DonePadClient.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string ToPasswordString(this string text)
        {
            if (text.IsNullOrWhiteSpace()) return null;
            var passwordString = text.Select(p => '*').ToString();
            return passwordString;
        }
        public static string ToMd5EncryptString(this string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(text));
            return Encoding.Default.GetString(result);
        }
    }
}