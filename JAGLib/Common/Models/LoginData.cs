using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class LoginData
    {
        public string _username { get; set; }
        public string _password { get; set; }
        public string _level { get; set; }
        public string _salt { get; set; }
        public string _hash { get; set; }
        public string _personId { get; set; }
    }
}