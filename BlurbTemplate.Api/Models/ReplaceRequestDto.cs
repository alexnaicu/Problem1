using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlurbTemplate.Api.Models
{
    public class ReplaceRequestDto
    {
        [Required]
        [StringLength(50000, ErrorMessage = "The template text length must be between 1 and 50000 characters", MinimumLength = 1)]
        public string Template { get; set; }        
        [Required]
        public string Placeholder { get; set; }        
        public string Replacement { get; set; }        
        public string SeparatorStart { get; set; }        
        public string SeparatorEnd { get; set; }
    }
}
