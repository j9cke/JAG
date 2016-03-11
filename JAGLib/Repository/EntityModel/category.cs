using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.EntityModel
{
    public class category
    {
        public int categoryId { get; set; }
        public string categoryt { get; set; }
        public int period { get; set; }
        public int penaltyperday { get; set; }
    }
}