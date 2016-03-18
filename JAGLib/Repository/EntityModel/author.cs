using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class author
    {
        public int _id { get; set; }
        public string _firstname { get; set; }
        public string _lastname { get; set; }
        public string _birthyear { get; set; }
    }
}