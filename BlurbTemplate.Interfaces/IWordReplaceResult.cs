using System;

namespace BlurbTemplate.Interfaces
{
    /// <summary>
    /// An interface for a type that encapsulates the result of a word replace operation
    /// </summary>
    public interface IWordReplaceResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string Value { get; }
    }
}
