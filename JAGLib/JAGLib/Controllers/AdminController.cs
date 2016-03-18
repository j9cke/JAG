using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace JAGLibrary.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

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
            if ((string)Session["name"] == "Admin")
                return View("AddAuthor", "_StandardLayout");
            else if(Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult ListAuthors()
        {
            if (Session["name"] == "Admin")
             {
                 var model = new ListAuthor();
                 model._authList = Service.Services.AuthorServices.getAuthorList();

                 return View("ListAuthors", "_StandardLayout", model);
             }
             else if (Session["user"] != null)
                 return Redirect("/Borrower/Borrower/");
             else
                 return Redirect("/Home/Login/");
            
        }

        public ActionResult EditAuthor(int aid)
        {
             if (Session["name"] == "Admin")
             {
                var model = Service.Services.AuthorServices.getAuthorFromAid(aid);
                return View("EditAuthor", "_StandardLayout", model);
             }
             else if (Session["user"] != null)
                 return Redirect("/Borrower/Borrower/");
             else
                 return Redirect("/Home/Login/");
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
            if (Session["name"] == "Admin")
            {
                List<SelectListItem> categoryId = new List<SelectListItem>();
                categoryId.Add(new SelectListItem { Text = "Extern", Value = "1" });
                categoryId.Add(new SelectListItem { Text = "Staff", Value = "2" });
                categoryId.Add(new SelectListItem { Text = "Student", Value = "3" });
                categoryId.Add(new SelectListItem { Text = "Child", Value = "4" });
                ViewData["Select Category"] = categoryId;

                var model = new Borrower();
                return View("AddBorrower", "_StandardLayout", model);
            }
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult EditBorrower(string bid)
        {
            if (Session["name"] == "Admin")
            {
                var model = Service.Services.BorrowerService.getBorrower(bid);

                return View("EditBorrower", "_StandardLayout", model);
            }
            else if(Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult ListBorrowers()
        {
            if (Session["name"] == "Admin")
            {
                var model = new ListBorrower();
                model._borrList = Service.Services.BorrowerService.getBorrowerList();

                return View("ListBorrowers", "_StandardLayout", model);
            }
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult AddBook()
        {
            if (Session["name"] == "Admin")
              return View("AddBook", "_StandardLayout");
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult EditBook(string s)
        {
            if (Session["name"] == "Admin")
            {
                var model = Service.Services.BookServices.getBookFromISBN(s);
                model._authorid = Service.Services.AuthorServices.getBookAuthorOfBook(s);

                return View("EditBook", "_StandardLayout", model);
            }
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult ListBooks()
        {
            if (Session["name"] == "Admin")
            {
                var model = new ListBook();
                model._bookList = Service.Services.BookServices.getBookList();

                return View("ListBooks", "_StandardLayout", model);
            }
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        public ActionResult Remove(int aid, string isbn, string bid)
        {
            if (Session["name"] == "Admin")
            {
                var model = new Remove();
                if (aid != 0)
                {
                    model._cat = 1;
                    model._author = Service.Services.AuthorServices.getAuthorFromAid(aid);
                    return View("Remove", "_StandardLayout", model);
                }
                else if (isbn != "0")
                {
                    model._cat = 2;
                    model._book = Service.Services.BookServices.getBookFromISBN(isbn);
                    return View("Remove", "_StandardLayout", model);
                }
                else
                {
                    model._cat = 3;
                    model._borrower = Service.Services.BorrowerService.getBorrower(bid);
                    return View("Remove", "_StandardLayout", model);
                }
            }
            else if (Session["user"] != null)
                return Redirect("/Borrower/Borrower/");
            else
                return Redirect("/Home/Login/");
        }

        //[HttpGet]
        public ActionResult AddBorrowerForm(Common.Models.Borrower m) 
        {
            List<Borrower> borrowerList = Service.Services.BorrowerService.getBorrowerList();
            var conf = new ConfirmationAdmin();
            if (!borrowerList.Exists(x => x._pid == m._pid))
            {
                saveBorrower(m);
                
                conf._Type = 2;
                conf._message = "Succesfully added borrower: ";
                conf._firstName = m._firstname;
                conf._lastName = m._lastname;

                return View("Confirmation", "_StandardLayout", conf);
            }
         
            conf._message = "Borrower not added. A borrower with the same person-ID already exist.";
            return View("Confirmation", "_StandardLayout", conf);
        }

        //[HttpGet]
        public ActionResult EditBorrowerForm(Common.Models.Borrower m)
        {
            var conf = new ConfirmationAdmin();
            conf._firstName = m._firstname;
            conf._lastName = m._lastname;

            Service.Services.BorrowerService.EditBorrower(m);
            conf._message = "Succesfully edited borrower: ";

            return View("Confirmation", "_StandardLayout", conf);
        }

        //[HttpGet]
        public ActionResult AddAuthorForm(Common.Models.Author m)
        {
            var conf = new ConfirmationAdmin();
            Service.Services.AuthorServices.addAuthorToDb(m);

            conf._firstName = m._firstname;
            conf._lastName = m._lastname;
            conf._message = "Succesfully added author: ";

            return View("Confirmation", "_StandardLayout", conf);
        }

        //[HttpGet]
        public ActionResult EditAuthorForm(Common.Models.Author m)
        {
            Service.Services.AuthorServices.EditAuthor(m);

            var conf = new ConfirmationAdmin();
            conf._firstName = m._firstname;
            conf._lastName = m._lastname;
            conf._message = "Succesfully edited author: ";

            return View("Confirmation", "_StandardLayout", conf);
        }

        //[HttpGet]
        public ActionResult AddBookForm(Common.Models.Book m)
        {
            List<Author> authorList = Service.Services.AuthorServices.getAuthorList();
            List<Book> bookList = Service.Services.BookServices.getBookList();
            var conf = new ConfirmationAdmin();
            conf._message = "";

            if (bookList.Exists(x => x._isbn == m._isbn)) {
                conf._message = "A book with the ISBN you entered already exist. ";
            }

            if (!authorList.Exists(x => x._id == m._authorid)) { 
                conf._message += "No Author with the Author ID you entered.";
            }
            
            else {
                Service.Services.BookServices.addBookToDb(m);

                conf._title = m._title;
                conf._Type = 0;
                conf._message = "Succesfully added book: ";
            }
            
            return View("Confirmation", "_StandardLayout", conf);
        }

        //[HttpGet]
        public ActionResult EditBookForm(Common.Models.Book m)
        {
            List<Author> authorList = Service.Services.AuthorServices.getAuthorList();
            var conf = new ConfirmationAdmin();
            
            if (!authorList.Exists(x => x._id == m._authorid)) {
                conf._message += "No Author with the Author ID you entered.";
            }

            else {
                Service.Services.BookServices.EditBook(m);

                conf._title = m._title;
                conf._Type = 0;
                conf._message = "Succesfully edited book: ";
            }

            return View("Confirmation", "_StandardLayout", conf);
        }

        public ActionResult RemoveThis(int cat, int aid, string isbn, string bid)
        {
            var conf = new ConfirmationAdmin();
            if (cat == 1) {
                AuthorDetails ad = Service.Services.AuthorServices.getBooksFromAuthor(aid);
                bool ok = true;
                foreach (Book b in ad._bookList)
                    if (Service.Services.BookServices.haveCopysOnLoan(b._isbn))
                        ok = false;

                if (ok)
                {
                    Service.Services.AuthorServices.Remove(aid);
                    conf._message = "Succesfully deleted author.";
                }
                else
                    conf._message = "Can not delete author. Some of the books written by this author is still on loan. They must be returned before deleting this author.";
            } else if (cat == 2) {
                if (!Service.Services.BookServices.haveCopysOnLoan(isbn))
                {
                    Service.Services.BookServices.Remove(isbn);
                    conf._message = "Succesfully deleted book.";
                }
                else
                    conf._message = "Can not remove this book. Some of the book copies are still on loan. They must be returned before deleting this book.";
            } else {
                if (!Service.Services.BorrowerService.haveBorrows(bid)) {
                    conf._message = "Succesfully deleted borrower.";
                    Service.Services.BorrowerService.Remove(bid);
                }
                else
                    conf._message = "The borrower has not returned all of the borrowed books and can therefor not be deleted.";
            }

            return View("Confirmation", "_StandardLayout", conf);
        }     
    }
}