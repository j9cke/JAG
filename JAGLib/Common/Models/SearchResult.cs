using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class SearchResult
    {
        //All svarsinformation från sökningen
        public List<Book> bList = new List<Book>();
        public List<Author> aList = new List<Author>();
    }
}