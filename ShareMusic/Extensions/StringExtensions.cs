using System.Text.RegularExpressions;

namespace ShareMusic.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceCaseInsensitive(this string input, string search, string replacement) 
        {
            string result = Regex.Replace(
                input,
                Regex.Escape(search),
                replacement.Replace("$", "$$"),
                RegexOptions.IgnoreCase);

            return result;
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string input) 
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            return false;
        }
    }
}
