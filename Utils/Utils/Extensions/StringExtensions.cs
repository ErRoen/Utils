using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utils.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.Contains(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool EndsWithOneOf(this string value, params string[] endings)
        {
            return endings.Any(ending => value.EndsWith(ending, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        ///     Converts a string to a Base64 encoded string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>'value' encoded as a Base64 string.</returns>
        public static string ToBase64(this string value)
        {
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     Decodes a Base64 encoded string.
        /// </summary>
        /// <param name="value">The Base64 encoded string to decode.</param>
        /// <returns>'value' decoded.</returns>
        public static string FromBase64(this string value)
        {
            var encoding = new UTF8Encoding();
            var bytes = Convert.FromBase64String(value);
            return encoding.GetString(bytes);
        }

        /// <summary>
        ///     Determines whether the string is numeric or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if string is numeric, otherwise False.</returns>
        public static bool IsNumeric(this string value)
        {
            return double.TryParse(value, NumberStyles.Number, null, out _);
        }

        /// <summary>
        ///     Determins whether the string contains any letter characters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <returns>True if the string contains any letters, false if not.</returns>
        public static bool HasLetters(this string value)
        {
            if (value == null)
                return false;

            for (var i = 0; i < value.Length; i++)
                if (char.IsLetter(value, i))
                    return true;

            return false;
        }

        /// <summary>
        ///     Determins whether the string contains any digit (0-9) characters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <returns>True if the string contains any digits, false if not.</returns>
        public static bool HasDigits(this string value)
        {
            if (value == null)
                return false;

            for (var i = 0; i < value.Length; i++)
                if (char.IsDigit(value, i))
                    return true;

            return false;
        }

        /// <summary>
        ///     Determines whether the string contains any special (non-letter, non-digit) characters.
        /// </summary>
        /// <param name="value">The string to search.</param>
        /// <returns>True if the string contains any special characters, false if not.</returns>
        public static bool HasSpecialChars(this string value)
        {
            if (value == null)
                return false;

            for (var i = 0; i < value.Length; i++)
                if (!char.IsLetterOrDigit(value, i))
                    return true;

            return false;
        }

        /// <summary>
        ///     Strip all HTML from a given string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StripHTML(this string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }

        public static bool IsFile(this string value)
        {
            var extension = Path.GetExtension(value);
            if (!extension.IsNullOrEmpty())
            {
                extension = extension.Replace(".", "");
                return !extension.IsNumeric();
            }

            return false;
        }

        public static string EscapeMarkDownCharacters(this string str)
        {
            var markdownSpecialChars = new List<string>
                {"\\", "`", "*", "_", "{", "}", "[", "]", "(", ")", "#", "+", "-", ".", "!"};
            markdownSpecialChars.ForEach(x => { str = str.Replace(x, @"\" + x); });

            return str;
        }

        /// <summary>
        ///     Determines if this is a numeric string using specified numeric style
        /// </summary>
        /// <returns>True if numeric</returns>
        public static bool IsNumeric(this string input, NumberStyles numberStyle)
        {
            return double.TryParse(input, numberStyle, CultureInfo.CurrentCulture, out _);
        }

        /// <summary>
        ///     Determines if this is a date string based on given format
        /// </summary>
        /// <returns>True if numeric</returns>
        public static bool IsDate(this string input, string format = null)
        {
            return format.IsNullOrEmpty() 
                ? DateTime.TryParse(input, out _) 
                : DateTime.TryParseExact(input, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        ///     Determines if a string is a direction (e.g. North, South, etc...)
        /// </summary>
        /// <returns>True if a compass direction</returns>
        public static bool IsDirection(this string input)
        {
            var validValues = new List<string>
            {
                "NORTH",
                "NORTHEAST",
                "NORTHWEST",
                "SOUTH",
                "SOUTHEAST",
                "SOUTHWEST",
                "EAST",
                "WEST",
                "N",
                "NE",
                "NW",
                "S",
                "SE",
                "SW",
                "E",
                "W",
                "N.",
                "S.",
                "E.",
                "W."
            };

            return input.ToUpper()
                .IsContainedIn(validValues);
        }

        /// <summary>
        ///     Strips all characters except numeric, alpha and spaces
        /// </summary>
        /// <returns>Stripped string</returns>
        public static string StripAllNonAlphaNumeric(this string input)
        {
            return new Regex("[^a-zA-Z0-9 -]")
                .Replace(input, "");
        }

        /// <summary>
        ///     Trims the supplied string, or returns an empty string if the value
        ///     is null.
        /// </summary>
        /// <param name="value">String to trim.</param>
        /// <returns>The trimmed string, if valid.  Otherwise an empty string.</returns>
        public static string NullSafeTrim(this string value)
        {
            return (value ?? string.Empty).Trim();
        }

        /// <summary>
        ///     Splits a camel case string into words.
        /// </summary>
        /// <param name="input">Input string to split.</param>
        /// <param name="capitalizeFirstWord">
        ///     If true, capitalizes the first
        ///     word in the series of words.
        /// </param>
        /// <returns>
        ///     A series of lowercase words derived from the original
        ///     camel case string.
        /// </returns>
        public static string SplitCamelCaseString(this string input, bool capitalizeFirstWord = true)
        {
            var sb = new StringBuilder();
            input = input.NullSafeTrim();

            if (string.IsNullOrEmpty(input))
                return string.Empty;

            sb.Append(capitalizeFirstWord ? char.ToUpper(input[0]) : char.ToLower(input[0]));
            for (var i = 1; i < input.Length; i++)
                if (i + 1 < input.Length &&
                    (input[i] == 'S' && input[i + 1] == 'L' ||
                     input[i] == 'I' && input[i + 1] == 'D'))
                {
                    // special case: SL and ID suffixex are ok, so keep it and
                    // advance to the next token
                    sb.Append(' ');
                    sb.Append(input[i++]);
                    sb.Append(input[i++]);
                }
                else if (char.IsUpper(input[i]))
                {
                    sb.Append(' ');
                    sb.Append(char.ToLower(input[i]));
                }
                else
                {
                    sb.Append(input[i]);
                }

            return sb.ToString();
        }

        public static string RemoveGoFromSql(this string sqlScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                sqlScript,
                @"^[\t ]*GO[\t ]*\d*[\t ]*(?:--.*)?$",
                RegexOptions.Multiline
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            var trimmed = statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));

            return trimmed.StringJoin("\n");
        }
    }
}