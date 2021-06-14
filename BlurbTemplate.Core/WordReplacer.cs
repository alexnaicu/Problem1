using System;
using System.Collections.Generic;
using System.Text;

using BlurbTemplate.Interfaces;

namespace BlurbTemplate.Implementation
{
    /// <summary>
    /// Provides placeholder replacement functionality
    /// </summary>
    public class WordReplacer : IWordReplacer
    {
        private const int MaxTemplateLength = 50000;

        /// <summary>
        /// Performs the replacement of all instances of <paramref name="placeholder"/> with instances of <paramref name="replacement"/> in the string <paramref name="template"/>
        /// </summary>
        /// <param name="template">The string on which replacements will be performed</param>
        /// <param name="placeholder">The word to be replaced</param>
        /// <param name="replacement">The replacement word for the placeholder</param>
        /// <param name="separatorStart">A string that marks the start of the placeholder word in the template text</paramref>/>
        /// <param name="separatorEnd">A string that marks the end of the placeholder word in the template text</paramref>/>
        /// <returns>A string representing the <paramref name="template"/> with <paramref name="placeholder"/> replaced by <paramref name="replacement"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public string ReplaceSinglePlaceholder(string template, string placeholder, string replacement, string separatorStart = "", string separatorEnd = "")
        {
            if (string.IsNullOrEmpty(placeholder))
                throw new ArgumentNullException(nameof(placeholder));

            var placeholdersWithReplacementsPairs = new Dictionary<string, string>() { { placeholder, replacement } };            

            return ReplaceMultiplePlaceholders(template, placeholdersWithReplacementsPairs, separatorStart, separatorEnd);            
        }

        /// <summary>
        /// Performs all the replacements provided in <paramref name="placeholdersWithReplacementsPairs"/> on the string <paramref name="template"/>
        /// </summary>
        /// <param name="template">The string on which replacements will be performed</param>
        /// <param name="placeholdersWithReplacementsPairs">A list of replacements to be made in KeyValuePair format <placeholder:replacement></param>
        /// <param name="separatorStart">A string that marks the start of the placeholder word in the template text</paramref>/>
        /// <param name="separatorEnd">A string that marks the end of the placeholder word in the template text</paramref>/>
        /// <returns>A string with all the replacements made</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public string ReplaceMultiplePlaceholders(string template, Dictionary<string, string> placeholdersWithReplacementsPairs, string separatorStart = "", string separatorEnd = "")
        {
            if (string.IsNullOrEmpty(template))
                throw new ArgumentNullException(nameof(template));

            if (template.Length > MaxTemplateLength)
                throw new ArgumentOutOfRangeException(nameof(template), $"The template text length exceeds the maximum allowed number of {MaxTemplateLength} characters for this type of operation.");

            if (placeholdersWithReplacementsPairs == null)
                throw new ArgumentNullException(nameof(placeholdersWithReplacementsPairs));

            if (separatorStart == null)
                throw new ArgumentNullException(nameof(separatorStart));

            if (separatorEnd == null)
                throw new ArgumentNullException(nameof(separatorEnd));

            var sb = new StringBuilder(template);

            foreach (var pair in placeholdersWithReplacementsPairs)
            {
                sb.Replace($"{separatorStart}{pair.Key}{separatorEnd}", pair.Value);
            }

            return sb.ToString();
        }
    }
}
