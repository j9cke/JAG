using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Mockup;

namespace JAGLibrary.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        Mockup mup = new Mockup();

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult AddAuthor()
        {
            return View("AddAuthor", "_StandardLayout");
        }

        public ActionResult ListAuthors()
        {
            return View("ListAuthors", "_StandardLayout");
        }

        public ActionResult EditAuthor()
        {
            var model = new Author();
            model._firstname = "Jonas";
            model._lastname = "Hitler";
            model._birthyear = 1932;

            return View("EditAuthor", "_StandardLayout", model);
        }

        public ActionResult AddBorrower()
        {
            return View("AddBorrower", "_StandardLayout");
        }

        public ActionResult EditBorrower()
        {
            var model = new Borrower();
            model._pid = 9312097711;
            model._firstname = "Adam";
            model._lastname = "Eriksson";
            model._address = "Stockholmsvägen 3";
            model._phoneno = "0762393349";
            model._catId = 1;

            return View("EditBorrower", "_StandardLayout", model);
        }

        public ActionResult ListBorrowers()
        {
            return View("ListBorrowers", "_StandardLayout");
        }

        public ActionResult AddBook()
        {
            return View("AddBook", "_StandardLayout");
        }

        public ActionResult EditBook()
        {
            var model = new Book();
            model._isbn = 9789137144238; 
            model._title = "Bli vän med din pms"; 
            model._signId = 1;
            model._publicationYear = 2015; 
            model._publicationInfo = "Varför är det så svårt att prata om pms? Medan man med lätthet talar om laktosintolerans, migrän och nageltrång kan det kännas pinsamt att berätta om sina premenstruella besvär och även att söka hjälp. Det vill Lisa Eriksson råda bot på.";
            model._pages = 232;

            return View("EditBook", "_StandardLayout", model);
        }

        public ActionResult ListBooks()
        {
            return View("ListBooks", "_StandardLayout");
        }

        //[HttpGet]
        public ActionResult AddAuthorForm(Common.Models.Author m)
        {
            //Skicka in authormodellen till databasen
            mup.authorList.Add(m);

            return View("AddAuthor", "_StandardLayout", m);
        }

        //[HttpGet]
        public ActionResult EditAuthorForm(Common.Models.Author m)
        {
            //Skicka in ny authormodellen till databasen
            mup.authorList.Add(m);

            return View("AddAuthor", "_StandardLayout", m);
        }
    }
}