using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class logindata
    {
        public string _username { get; set; }
        public string _password { get; set; }
        public string _level { get; set; }
        public string _salt { get; set; }
    }
}