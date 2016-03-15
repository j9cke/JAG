using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Borrower
    {
        [Required(ErrorMessage = "A swedish 'Personnummer' is required")]
        [RegularExpression(@"\d{8}-\d{4}", ErrorMessage = "Must be on the form yyyymmdd-xxxx")]
        public string _pid { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        [RegularExpression(@"^[A-ZÅÖÄa-zåäö0-9!#¤%&=?@£,;*-_<>]{7,50}", ErrorMessage = "Must be atleast 7 characters")]
        public string _password { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        public string _firstname { get; set; }
       
        [Required(ErrorMessage = "A name is required.")]
        public string _lastname { get; set; }

        [Required(ErrorMessage = "An address is required.")]
        public string _address { get; set; }

        [Required(ErrorMessage = "A phonenumber is required.")]
        [RegularExpression(@"^[0]{1}[0-9-]{6,15}", ErrorMessage = "Must start on a 0")]
        public string _phoneno { get; set; }

        public int _catId { get; set; }
    }
}