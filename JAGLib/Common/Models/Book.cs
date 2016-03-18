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
        [RegularExpression(@"\w{4,12}",ErrorMessage="The ISBN need to be between 4 and 12 characters long.")]
        public string _isbn { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        [RegularExpression(@"^[A-ZÅÄÖa-zåäö0-9\s-]+", ErrorMessage="The title can only contain normal characters.")]
        public string _title { get; set; }

        [Required(ErrorMessage = "A Sign ID is required.")]
        [RegularExpression(@"^([1-5]?[0-9]|6[0-5])$)", ErrorMessage = "The Sign ID need to be between 1 and 70.")]
        public int _signId { get; set; }
        
        [Required(ErrorMessage = "A publication year is required.")]
        [RegularExpression(@"\d{4}", ErrorMessage="Must be 4 digits.")]
        public string _publicationYear { get; set; }

        [Required(ErrorMessage = "Publication info is required.")]
        [RegularExpression(@"^[A-ZÅÄÖa-zåäö0-9\s-]+", ErrorMessage="Publication info can only contain normal characters.")]
        public string _publicationInfo { get; set; }

        [Required(ErrorMessage = "Number of pages is required.")]
        [RegularExpression(@"\d+", ErrorMessage="Can only contain digits")]
        public int _pages { get; set; }

        [Required(ErrorMessage = "An author ID is required.")]
        [RegularExpression(@"\d{3,6}", ErrorMessage="An author ID must be between 3-6 digits.")]
        public int _authorid { get; set; }
    }
}