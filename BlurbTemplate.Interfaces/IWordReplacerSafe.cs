using System;
using System.Collections.Generic;

namespace BlurbTemplate.Interfaces
{
    /// <summary>
    /// A word replacer interface that provides the result in a IWordReplaceResult object    
    /// </summary>
    public interface IWordReplacerSafe
    {
        IWordReplaceResult ReplaceSinglePlaceholder(string template, string placeholder, string replacement, string placeholderSeparatorStart, string placeholderSeparatorEnd);

        IWordReplaceResult ReplaceMultiplePlaceholders(string template, Dictionary<string, string> placeholdersWithReplacementsPairs, string placeholderSeparatorStart, string placeholderSeparatorEnd);
    }
}
