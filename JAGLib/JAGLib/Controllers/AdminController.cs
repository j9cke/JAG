using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JAGLibrary.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
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

        public ActionResult ListBorrowers()
        {
            return View("ListBorrowers", "_StandardLayout");
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
    }
}