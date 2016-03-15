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
        //public HttpResponseMessage GetListOfBooks(string s)
        //{
        //    // Code handling GET requests for /api/Book
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, BookServices.searchBook(s));
        //    response.ReasonPhrase = "OK";
        //    return response;
        //}

        public string Search(string id)
        {
            return JsonConvert.SerializeObject(BookServices.searchBook(id));
        }
    }
}
