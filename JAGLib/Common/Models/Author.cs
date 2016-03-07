using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Author
    {
        public int _id { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string _firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string _lastname { get; set; }

        [Required(ErrorMessage = "Birthyear is required.")]
        [MinLength(4), MaxLength(4)]
        public string _birthyear { get; set; }
    }
}