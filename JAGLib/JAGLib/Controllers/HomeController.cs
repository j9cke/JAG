using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Mockup;
using Service;
using System.Text.RegularExpressions;


namespace JAGLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Search();
            return View("Index", "_StandardLayout", model);
        }

        public ActionResult Author(int aid)
        {
            var model = Service.Services.AuthorServices.getBooksFromAuthor(aid);
            model.author_aid = aid.ToString();
            return View("Author", "_StandardLayout", model);
        }

        public ActionResult Browse()
        {
            var br = new Browse();
            var s = new Search();
            br._search = s;
            var model = new BrowseResult();
            model._browse = br;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult BrowseBy(string b)
        {
            var br = new Browse();
            var s = new Search();
            br._search = s;
            br._browseBy = b;
            var model = new BrowseResult();
            model._browse = br;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult BrowseLetterBy(string s, string by)
        {
            if (by == "Author")
            {
                var model = new BrowseResult();
                var b = new Browse();
                b._browseBy = by;
                model._browse = b;
                model._letter = s;
                model._aList = Service.Services.AuthorServices.getAuthorListFromFirstLetter(s);

                return View("BrowseResult", "_BrowseLayout", model);
            }

            else if (by == "Book")
            {
                var model = new BrowseResult();
                var b = new Browse();
                b._browseBy = by;
                model._browse = b;
                model._letter = s;

                model._bList = Service.Services.BookServices.getBookListOnFirstLetter(s);

                return View("BrowseResult", "_BrowseLayout", model);
            }

            return View();
        }

        public ActionResult Book(string isbn)
        {
            List<BookDetails> bdList = Service.Services.BookServices.getBookDetailsFromIsbn(isbn);
            var model = new BookDetails();
            
            bool fixString = false;
            foreach (BookDetails bd in bdList) {
                if (fixString)
                    model.authorstring = model.authorstring + ", " + bd.author_firstname + " " + bd.author_lastname;
                else {
                    model.authorstring = bd.author_firstname + " " + bd.author_lastname;
                    fixString = true;
                }
            }
           
            foreach (Copy c in bdList[0]._copyList)
            {            
                switch (c._library)
                {
                    case "Stadsbiblioteket":
                        model._stadsbiblioteket++;
                        break;
                    case "Taberg":
                        model._taberg++;
                        break;
                    case "Huskvarna":
                        model._huskvarna++;
                        break;
                }
            }

            model.book_isbn = bdList[0].book_isbn;
            model.book_title = bdList[0].book_title;
            model.book_signId = bdList[0].book_signId;
            model.book_publicationYear = bdList[0].book_publicationYear;
            model.book_publicationInfo = bdList[0].book_publicationInfo;
            model.book_pages = bdList[0].book_pages;
            model._copyList = bdList[0]._copyList;

            return View("Book", "_StandardLayout", model);
        }

        public ActionResult Login()
        {
            var model = new LoginData();
            return View("Login", "_StandardLayout", model);
        }

        public ActionResult Logout() 
        {
            Session.Clear();
            var model = new LoginData();
            return View("Login", "_StandardLayout", model);
        }

        //[HttpGet]
        public ActionResult SearchFunc(Common.Models.Search m)
        {
            var sr = new SearchResult();
            string regExp = @"[^\w\d ?!.,-]";
            string tmp = Regex.Replace(m._searchData._searchString, regExp, "");
            sr.bList = Service.Services.BookServices.searchBook(tmp);
            sr.aList = Service.Services.AuthorServices.searchAuthor(tmp);
            m._searchResult = sr;

            return View("SearchResult", "_StandardLayout", m);
        }

        public string getHash(string str, string salt)
        {
            string hash = str + salt;
            return hash.GetHashCode().ToString(); ;
        }

        //[HttpGet]
        public ActionResult LoginFunc(Common.Models.LoginData m)
        {
            if (m._username == "admin")
                m._username = "99999999-9999";
         
            List<LoginData> userList = Service.Services.LoginService.getUserList();
            
            if (userList.Exists(x => x._username == m._username))
            { 
                if (userList.Find(x => x._username == m._username)._password == getHash(m._password, userList.Find(x => x._username == m._username)._salt))
                {
                    switch (userList.Find(x => x._username == m._username)._level)
                    {
                        case "1":
                            Session["user"] = userList.Find(x => x._username == m._username);
                            return Redirect("/Borrower/Borrower/");
        
                        case "2":
                            Session["user"] = userList.Find(x => x._username == m._username);
                            return Redirect("/Admin/Admin/");
  
                        default:
                            break;
                    }
                }
            }
            
            return View("Login", "_StandardLayout");
        }
    }
}