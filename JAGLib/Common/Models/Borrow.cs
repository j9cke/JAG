using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Borrow
    {
        public string _barcode { get; set; }
        public long _pid { get; set; }
        public string _borrowDate { get; set; }
        public string _toBeReturnedDate { get; set; }
        public string _returnDate { get; set; }
    }
}