using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Search
    {
        public SearchData _searchData { get; set; }
        public SearchResult _searchResult { get; set; }

        public Search()
        {
            _searchData = new SearchData();
            _searchResult = new SearchResult();
        }
    }
}