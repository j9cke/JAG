using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class copy
    {
        public int copy_status { get; set; }
        public string copy_barcode { get; set; }
        public string copy_location { get; set; }
        public string copy_isbn { get; set; }
        public string copy_library { get; set; }
        public int copy_available { get; set; }
    }
}