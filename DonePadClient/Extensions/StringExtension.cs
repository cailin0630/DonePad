using System.Linq;

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
    }
}