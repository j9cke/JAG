using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Category
    {
        public int catId { get; set; }
        public string categoryType { get; set; }
        public int period { get; set; }
        public int penalty { get; set; }
    }
}