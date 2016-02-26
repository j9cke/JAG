using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class SearchData
    {
        public string _searchString { get; set; }
        public bool _byAuthor { get; set; }
        public bool _byTitle { get; set; }
    }
}