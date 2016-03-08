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
            if (Session["user"] == null)
                    return Redirect("/Home/Login/");
            else 
            {
                LoginData user = (LoginData)Session["user"];
                if (user._level == "1")
                    return Redirect("/Borrower/Borrower/");
                else if (user._level == "2")
                {
                    Session["name"] = "Admin";
                    return View();
                }
                else return Redirect("/Home/Login/");
            }
        }

        public ActionResult AddAuthor()
        {
            return View("AddAuthor", "_StandardLayout");
        }

        public ActionResult ListAuthors()
        {
            var model = new ListAuthor();
            model._authList = Service.Services.AuthorServices.getAuthorList();

            return View("ListAuthors", "_StandardLayout", model);
        }

        public ActionResult EditAuthor()
        {
            var model = new Author();
            model._firstname = "Jonas";
            model._lastname = "Hitler";
            model._birthyear = "1932";

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


        private void saveBorrower(Common.Models.Borrower m)
        {
            int saltLenght = 32;
            LoginData ld = new LoginData();

            ld._salt = getSalt(saltLenght);
            ld._password = getHash(m._password, ld._salt); ;
            ld._username = m._pid;
            ld._level = "1";
            //ld._hash = getHash(m._password, ld._salt);

            //Add to database.
            Borrower person = new Borrower();
            person._pid = m._pid;
            person._firstname = m._firstname;
            person._lastname = m._lastname;
            person._address = m._address;
            person._phoneno = m._phoneno;
            person._catId = m._catId;

            //add person to database
            Service.Services.BorrowerService.addBorrowerToDb(person);
            Service.Services.LoginService.addUserToDb(ld);
        } 

        
        public ActionResult AddBorrower()
        {
            var model = new Borrower();
            return View("AddBorrower", "_StandardLayout", model);
        }


        //[HttpGet]
        public ActionResult AddBorrowerForm(Common.Models.Borrower m) 
        {
            saveBorrower(m);

            return View("AddBorrower", "_StandardLayout");
        }



        public ActionResult EditBorrower()
        {
            var model = new Borrower();
            model._pid = "9312097711";
            model._firstname = "Adam";
            model._lastname = "Eriksson";
            model._address = "Stockholmsvägen 3";
            model._phoneno = "0762393349";
            model._catId = "1";

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

        public ActionResult EditBook(string s)
        {
            var model = Service.Services.BookServices.getBookFromISBN(s);

            return View("EditBook", "_StandardLayout", model);
        }

        public ActionResult ListBooks()
        {
            var model = new ListBook();
            model._bookList = Service.Services.BookServices.getBookList();

            return View("ListBooks", "_StandardLayout", model);
        }

        //[HttpGet]
        public ActionResult AddAuthorForm(Common.Models.Author m)
        {            
            //GÖR VERIFIERING FÖRST
            Service.Services.AuthorServices.addAuthorToDb(m);

            return View("AddAuthor", "_StandardLayout", m);
        }

        //[HttpGet]
        public ActionResult EditAuthorForm(Common.Models.Author m)
        {
            //Skicka in ny authormodellen till databasen
            mup.authorList.Add(m);

            return View("AddAuthor", "_StandardLayout", m);
        }

        //[HttpGet]
        public ActionResult AddBookForm(Common.Models.Book m)
        {
            //GÖR VERIFIERING FÖRST
            Service.Services.BookServices.addBookToDb(m);

            return View("AddBook", "_StandardLayout", m);
        }
    }
}