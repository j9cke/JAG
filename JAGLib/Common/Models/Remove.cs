using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Remove
    {
        public int _cat { get; set; }
        public Author _author { get; set; }
        public Book _book { get; set; }
        public Borrower _borrower { get; set; }
    }
}