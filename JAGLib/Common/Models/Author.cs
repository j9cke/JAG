using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Author
    {
        public int _id { get; set; }
        public string _firstname { get; set; }
        public string _lastname { get; set; }
        public int _birthyear { get; set; }

        public Author()
        {
        }
    }
}