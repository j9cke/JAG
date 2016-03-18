using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class authordetails
    {
        public string author_firstname { get; set; }
        public string author_lastname { get; set; }
        public string author_birthyear { get; set; }
        public string author_aid { get; set; }

        public List<book> _bookList = new List<book>();
    }
}