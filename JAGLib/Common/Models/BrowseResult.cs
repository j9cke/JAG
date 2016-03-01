using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class BrowseResult
    {
        public List<Author> _aList = new List<Author>();
        public List<Book> _bList = new List<Book>();
        public Book _book;
        public Author _author;
        public string _title;
        public Browse _browse;
        public string _letter;
    }
}