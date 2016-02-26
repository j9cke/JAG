using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Copy
    {
        public bool _status { get; set; }
        public string _barcode { get; set; }
        public string _location { get; set; }
        public long _isbn { get; set; }
        public string _library { get; set; }
    }
}