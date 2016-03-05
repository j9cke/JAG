using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class bookdetails
    {
        public string book_isbn { get; set; }
        public string book_title { get; set; }
        public int book_signId { get; set; }
        public string book_publicationYear { get; set; }
        public string book_publicationInfo { get; set; }
        public int book_pages { get; set; }

        public string author_firstname { get; set; }
        public string author_lastname { get; set; }
    }
}