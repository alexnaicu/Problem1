using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlurbTemplate.Implementation.Tests.Utils
{
    public static class RandomTextGenerator
    {
        /// <summary>
        /// Generates a text of random words and with a placeholder inserted at certain positions 
        /// Also generates a Stringbuilder that holds the template with the replacement in place of placeholder, at the same positions
        /// </summary>
        /// <param name="wordNumber">Number of random generated words</param>
        /// <param name="placeholder">The placeholder that is inserted at certain positions</param>
        /// <returns>A text that contains random words and some placeholders/ A stringbuilder with the replacement in place of placeholder </returns>
        public static (string, StringBuilder) GetRandomTextWithSinglePlaceholder(int wordNumber, (string, string) placeholderWithReplacementPair)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var words = new string[wordNumber];
            for (var i = 0; i < wordNumber; i++)
            {
                words[i] = new string(Enumerable.Range(1, random.Next(3, 10)).Select(_ => chars[random.Next(chars.Length)]).ToArray());
            }

            var sbWithPlaceholders = new StringBuilder("Random words:");
            var sbWithReplacements = new StringBuilder("Random words:");
            sbWithPlaceholders.AppendLine();
            sbWithReplacements.AppendLine();

            var placeholder = placeholderWithReplacementPair.Item1;
            var replacement = placeholderWithReplacementPair.Item2;

            for (var i = 0; i < wordNumber; i++)
            {
                if (i % 3 == 0 && i % 5 == 0) // 0, 15, 30, 45, 60, 75, ..
                {
                    sbWithPlaceholders.Append(placeholder)
                        .Append(' ');
                    sbWithReplacements.Append(replacement)
                        .Append(' ');
                }

                sbWithPlaceholders.Append(words[i])
                    .Append(' ');
                sbWithReplacements.Append(words[i])
                    .Append(' ');
            }

            return (sbWithPlaceholders.ToString(), sbWithReplacements);
        }
    }
}
