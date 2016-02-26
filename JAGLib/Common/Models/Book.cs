using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class Book
    {
        public long _isbn { get; set; }
        public string _title { get; set; }
        public int _signId { get; set; }
        public int _publicationYear { get; set; }
        public string _publicationInfo { get; set; }
        public int _pages { get; set; }
    }
}