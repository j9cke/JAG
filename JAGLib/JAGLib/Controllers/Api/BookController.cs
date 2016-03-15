using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Services;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JAGLib.Controllers.Api
{
    public class BookController : Controller
    {
        public string Search(string id)
        {
            return JsonConvert.SerializeObject(BookServices.searchBook(id));
        }
    }
}
