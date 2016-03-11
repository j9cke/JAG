using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Book
    {
        [Required(ErrorMessage = "A ISBN is required.")]
        public string _isbn { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string _title { get; set; }

        [Required(ErrorMessage = "A Sign ID is required.")]
        [Range(1, 65)]
        public int _signId { get; set; }
        
        [Required(ErrorMessage = "A publication year is required.")]
        [MinLength(4), MaxLength(4)]
        public string _publicationYear { get; set; }

        [Required(ErrorMessage = "Publication info is required.")]
        public string _publicationInfo { get; set; }

        [Required(ErrorMessage = "Number of pages is required.")]
        public int _pages { get; set; }

        [Required(ErrorMessage = "An author ID is required.")]
        public int _authorid { get; set; }
    }
}