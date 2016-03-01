using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class BookDetails
    {
        public Book _book { get; set; }
        public Classification _classification { get; set; }
        public Author _author { get; set; }
        public Copy _copy { get; set; }

        public BookDetails()
        {
            _book = new Book();
            _classification = new Classification();
            _author = new Author();
        }
    }
}