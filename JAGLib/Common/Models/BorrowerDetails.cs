using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class BorrowerDetails
    {
        public string _pid { get; set; }
        public string _firstname { get; set; }
        public string _lastname { get; set; }
        public string _address { get; set; }
        public string _phoneno { get; set; }
        public int _catId { get; set; }

        public List<Borrow> _borrowlist = new List<Borrow>();
    }
}