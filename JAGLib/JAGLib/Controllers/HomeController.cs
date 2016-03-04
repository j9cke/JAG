using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Mockup;
using Service;


namespace JAGLibrary.Controllers
{
    public class HomeController : Controller
    {
        Mockup mup = new Mockup();

        public ActionResult Index()
        {
            List<Author> fdfdf = Service.Services.AuthorServices.getEmployeeList();
            List<Borrower> adam = Service.Services.BorrowerService.getBorrowerList();

            var model = new Search();


            return View("Index", "_StandardLayout", model);
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

                //Hämta alla Authors som börjar på string s
                foreach (Author a in mup.authorList)
                {
                    if (a._lastname.StartsWith(s))
                    {
                        //lägg till i ny model 
                        model._aList.Add(a);
                    }
                }

                return View("BrowseResult", "_BrowseLayout", model);
            }

            else if (by == "Book")
            {
                var model = new BrowseResult();
                var b = new Browse();
                b._browseBy = by;
                model._browse = b;
                model._letter = s;

                //Hämta alla Authors som börjar på string s
                foreach (Book bk in mup.bookList)
                {
                    if (bk._title.StartsWith(s))
                    {
                        //lägg till i ny model 
                        model._bList.Add(bk);
                    }
                }

                return View("BrowseResult", "_BrowseLayout", model);
            }

            return View();
        }

        public ActionResult Book()
        {
            var book = new Book();
            var classification = new Classification();
            var author = new Author();
            var copy = new Copy();
            var mockup = new Mockup();

            for (int i = 0; i < mockup.copyList.Count(); i++)
            {
                if (mockup.copyList.Exists(x => x._status == true && x._isbn == 9789137144238))
                {
                    copy._available++;
                }
            }

            book._isbn = 9789137144238;
            book._pages = 200;
            book._publicationYear = 2016;
            book._title = "En shoppaholis mardröm";
            book._publicationInfo = "Bonnier";
            author._firstname = "Evert";
            author._lastname = "Taube";
            classification._description = "En bok om en shoppaholic vid namn Adam Tollin.";

            var model = new BookDetails();
            model._copy = copy;
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

        public ActionResult Logout() 
        {
            Session.Clear();
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