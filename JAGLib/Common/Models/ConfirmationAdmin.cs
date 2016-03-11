using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class ConfirmationAdmin
    {

        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _title { get; set; }
        public int _Type { get; set; } // 0 = Book, 1= Author, 2 = Borrower
        public string _message { get; set; }

        public Author _author { get; set; }
        public Book _book { get; set; }
        public Borrower _borrower { get; set; }

        public ConfirmationAdmin()
        {
            _author = new Author();
            _book = new Book();
            _borrower = new Borrower();
        }
    }
}