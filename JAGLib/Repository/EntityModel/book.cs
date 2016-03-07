using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class book
    {
        public string _isbn { get; set; }
        public string _title { get; set; }
        public int _signId { get; set; }
        public string _publicationYear { get; set; }
        public string _publicationInfo { get; set; }
        public int _pages { get; set; }
    }
}