using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class BookDetails
    {
        public string book_isbn { get; set; }
        public string book_title { get; set; }
        public int book_signId { get; set; }
        public string book_publicationYear { get; set; }
        public string book_publicationInfo { get; set; }
        public int book_pages { get; set; }
        public int _copies { get; set; }
        public int _stadsbiblioteket { get; set; }
        public int _taberg { get; set; }
        public int _huskvarna { get; set; }


        public List<Copy> _copyList = new List<Copy>();

        public string author_firstname { get; set; }
        public string author_lastname { get; set; }
        public string authorstring { get; set; }
    }
}