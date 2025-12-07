using System.Text.RegularExpressions;

namespace AnaPrevention.GeneralMasterData.Api.Common.Tools.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return value; }

            var regexMath = Regex.Match(value, @"^_+");

            return regexMath + Regex.Replace(value, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
