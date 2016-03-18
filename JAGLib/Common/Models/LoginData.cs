using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class LoginData
    {
        [Required(ErrorMessage = "A swedish 'Personnummer' is required")]
        [RegularExpression(@"\d{8}-\d{4}|admin", ErrorMessage = "Must be on the form yyyymmdd-xxxx")]
        public string _username { get; set; }

        public string _password { get; set; }
        public string _level { get; set; }
        public string _salt { get; set; }
        public string _hash { get; set; }
        public string _personId { get; set; }
    }
}