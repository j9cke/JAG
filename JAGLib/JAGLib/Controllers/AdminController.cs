using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using Service.Mockup;

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

        private string getSalt(int maxLength)
        {
            var salt = new byte[maxLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string getHash(string str, string salt)
        {
            string hash = str + salt;
            return hash.GetHashCode().ToString(); ;
        }

        public ActionResult AddBorrower(Common.Models.Borrower m)
        {
            int saltLenght = 32;
            LoginData ld = new LoginData();

            ld._salt = getSalt(saltLenght);
            ld._password = m._password;
            ld._username = m._pid.ToString();
            ld._level = "1";
            ld._hash = getHash(m._password, ld._salt);
            ld._personId = "5";
            
            //Add to database.
            Borrower person = new Borrower();
            person._firstname = m._firstname;
            person._lastname = m._lastname;
            person._address = m._address;
            person._phoneno = m._phoneno;
            person._catId = m._catId;

            //add person to database

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