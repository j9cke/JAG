using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Borrower
    {
        [Required(ErrorMessage = "Personal ID is required.")]
        public string _pid;

        [Required(ErrorMessage = "Password is required.")]
        public string _password;

        [Required(ErrorMessage = "Firstname is required.")]
        public string _firstname;

        [Required(ErrorMessage = "Lastname is required.")]
        public string _lastname;

        [Required(ErrorMessage = "Address is required.")]
        public string _address;

        [Required(ErrorMessage = "Phonenumber is required.")]
        public string _phoneno;

        public int _catId { get; set; }
        

        
    }
}