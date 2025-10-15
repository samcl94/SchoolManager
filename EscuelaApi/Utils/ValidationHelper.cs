using System.Text.RegularExpressions;

namespace SchoolApi.Utils
{
    public class ValidationHelper
    {
        private static readonly Regex DniNieRegex = new Regex(
            @"^(\d{8}[A-Za-z]|[XYZ]\d{7}[A-Za-z])$",
            RegexOptions.Compiled);

        public static bool IsValidDniNie(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return DniNieRegex.IsMatch(input);
        }
    }
}
