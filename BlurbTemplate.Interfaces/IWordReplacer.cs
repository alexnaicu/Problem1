using System;
using System.Collections.Generic;

namespace BlurbTemplate.Interfaces
{
    /// <summary>
    /// An interface for a type that can perform word replace operations
    /// </summary>
    public interface IWordReplacer
    {
        string ReplaceSinglePlaceholder(string template, string placeholder, string replacement, string separatorStart = "", string separatorEnd = "");

        string ReplaceMultiplePlaceholders(string template, Dictionary<string, string> placeholdersWithReplacementsPairs, string separatorStart = "", string separatorEnd = "");
    }
}
