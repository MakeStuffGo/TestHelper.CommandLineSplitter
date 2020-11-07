using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestHelpers
{
    // this class is a combination of this answer and comment
        // https://stackoverflow.com/a/24829691/572760
    public static class CommandLineSplitterManaged
    {
        public static IEnumerable<string> Split(this string str,
                                            Func<char, bool> controller)
        {
            int nextPiece = 0;

            for (int c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        private static Regex qoutesRegex = new Regex("(?<!\\\\)\\\"", RegexOptions.Compiled);

        public static string[] SplitCommandLine(string commandLine)
        {
            bool inQuotes = false;
            bool isEscaping = false;

            return commandLine.Split(c => {
                if (c == '\\' && !isEscaping) { isEscaping = true; return false; }

                if (c == '\"' && !isEscaping)
                    inQuotes = !inQuotes;

                isEscaping = false;

                return !inQuotes && Char.IsWhiteSpace(c)/*c == ' '*/;
            })
                .Select(arg => arg.Trim())
                .Select(arg => qoutesRegex.Replace(arg, "").Replace("\\\"", "\""))
                .Where(arg => !string.IsNullOrEmpty(arg))
                .ToArray();
        }

        public static string TrimMatchingQuotes(this string input, char quote)
        {
            if (input.Length >= 2 &&
                input[0] == quote && input[input.Length - 1] == quote)
                return input.Substring(1, input.Length - 2);

            return input;
        }
    }
}
