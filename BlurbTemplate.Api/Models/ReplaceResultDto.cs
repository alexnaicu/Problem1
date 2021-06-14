using System;

namespace BlurbTemplate.Api.Models
{
    public class ReplaceResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
