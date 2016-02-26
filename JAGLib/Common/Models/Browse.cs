using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Browse
    {
        public Search _search;
        public string _browseBy;

        public Browse()
        {
            _search = new Search();
            _browseBy = "Author";
        }
    }
}