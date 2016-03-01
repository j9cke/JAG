using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Mockup;


namespace JAGLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new Search();

            return View("Index", "_StandardLayout", model);
        }

        public ActionResult Browse()
        {
            var model = new Browse();
            var s = new Search();
            model._search = s;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult BrowseBy(string b)
        {
            var model = new Browse();
            var s = new Search();
            model._search = s;
            model._browseBy = b;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult Book()
        {
            var book = new Book();
            var classification = new Classification();
            var author = new Author();
            book._isbn = 19465811884;
            book._publicationYear = 2016;
            book._title = "En shoppaholis mardröm";
            author._firstname = "Evert";
            author._lastname = "Taube";
            book._publicationInfo = "Bonnier";
            classification._description = "En bok om en shoppaholic vid namn Adam Tollin.";

            var model = new BookDetails();
            model._book = book;
            model._classification = classification;
            model._author = author;
            return View("Book", "_StandardLayout", model);
        }

        public ActionResult Login()
        {
            var model = new LoginData();

            return View("Login", "_StandardLayout", model);
        }

        //[HttpGet]
        public ActionResult SearchFunc(Common.Models.Search m)
        {
            //skicka in sökdata
            var sr = new SearchResult();
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
            
            Mockup mockup = new Mockup();
            
            if (mockup.userList.Exists(x => x._username == m._username))
            {
                m._hash = getHash("123",  mockup.userList.Find(x => x._username == m._username)._salt);
                
                if (mockup.userList.Find(x => x._username == m._username)._hash == getHash(m._password, mockup.userList.Find(x => x._username == m._username)._salt))
                {
                    switch (mockup.userList.Find(x => x._username == m._username)._level)
                    {
                        case "1":
                            //Session["level"]="Borrower";
                            Session["pId"] = m._username;
                            return Redirect("/Borrower/Borrower/");
        
                        case "2":
                            Session["level"] = "Admin";
                            Session["pId"] = "Admin";
                            //return View("../Admin/Admin", "_StandardLayout");
                            return Redirect("/Admin/Admin/");

                            
                        default:
                            break;
                    }
                }
            }
            
            else
            {

            }

            return View("Login", "_StandardLayout");
        }
    }
}