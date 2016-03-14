using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Copy
    {
        public int _status { get; set; }
        public string _barcode { get; set; }
        public string _location { get; set; }
        public string _isbn { get; set; }
        public string _library { get; set; }
        public int _available { get; set; }
    }
}