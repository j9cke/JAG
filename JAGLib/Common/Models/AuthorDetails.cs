using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class AuthorDetails
    {
        public string author_firstname { get; set; }
        public string author_lastname { get; set; }
        public string author_birthyear { get; set; }
        public string author_aid { get; set; }

        public List<Book> _bookList = new List<Book>();
    }
}