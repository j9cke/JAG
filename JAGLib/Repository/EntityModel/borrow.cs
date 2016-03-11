using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class borrow
    {
        public string barcode { get; set; }
        public string pid { get; set; }
        public DateTime borrowDate { get; set; }
        public DateTime toBeReturnedDate { get; set; }
        public DateTime returnDate { get; set; }
        public string penalty { get; set; }

        public book book { get; set; }
    }
}