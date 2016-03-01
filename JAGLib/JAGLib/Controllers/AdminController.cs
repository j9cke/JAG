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