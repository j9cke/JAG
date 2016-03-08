using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Borrow
    {
        public string _barcode { get; set; }
        public string _pid { get; set; }
        public DateTime _borrowDate { get; set; }
        public DateTime _toBeReturnedDate { get; set; }
        public DateTime _returnDate { get; set; }

        public Book _book {get; set;}
    }
}