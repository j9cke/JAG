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
        [RegularExpression(@"^[0-9xX]{4-12}",ErrorMessage="The ISBN need to be between 4 and 12 characters long and can contain numbers.")]
        public string _isbn { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        [RegularExpression(@"^[A-ZÅÄÖa-zåäö-]")]
        public string _title { get; set; }

        [Required(ErrorMessage = "A Sign ID is required.")]
        [RegularExpression(@"^[0-9]{1-2}",ErrorMessage="The signID need to be between 1 and 65.")]
        [Range(1, 65)]
        public int _signId { get; set; }
        
        [Required(ErrorMessage = "A publication year is required.")]
        [RegularExpression(@"^[0-9]{4}", ErrorMessage="Can only contain numbers")]
        public string _publicationYear { get; set; }

        [Required(ErrorMessage = "Publication info is required.")]
        [RegularExpression(@"^[A-ZÅÄÖa-zåäö-]")]
        public string _publicationInfo { get; set; }

        [Required(ErrorMessage = "Number of pages is required.")]
        [RegularExpression(@"\d+", ErrorMessage="Can only contain numbers")]
        public int _pages { get; set; }

        [Required(ErrorMessage = "An author ID is required.")]
        [RegularExpression(@"\d+")]
        public int _authorid { get; set; }
    }
}