using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Models;
using Service.Mockup;

namespace JAGLib.Controllers
{
    public class BorrowerController : Controller
    {
        Mockup mup = new Mockup();

        public ActionResult Borrower()
        {
            if (Session["user"] == null)
                return Redirect("/Home/Login/");
            else {
                //hämta borrower från SQL
                
                LoginData user = (LoginData)Session["user"];
                string pId = user._username;
                Borrower b = Service.Services.BorrowerService.getBorrower(pId);
                Session["name"] = b._firstname + " " + b._lastname;

                //List<Borrow> borrow = Service.Services.BorrowerService.getPersonsBorrowList(model);
                BorrowerDetails model = Service.Services.BorrowerService.getBorrowerDetails(pId);
                if(model._pid == null)
                {
                    model._firstname = b._firstname;
                    model._lastname = b._lastname;
                    model._pid = b._pid;
                    model._phoneno = b._phoneno;
                    model._address = b._address;
                    model._catId = b._catId;
                }

                    
                return View("Borrower", "_StandardLayout", model);
            }

            
        }
        public ActionResult RenewLoan(string bar, string pid)
        {
            BorrowerDetails bd = Service.Services.BorrowerService.getBorrowerDetails(pid); 
            Category cat = Service.Services.BorrowerService.getCategory(bd._catId.ToString());
            Borrow brw = new Borrow();

            
            
            brw._barcode = bar;
            brw._pid = bd._pid;
            brw._borrowDate = DateTime.Now;
            brw._toBeReturnedDate = DateTime.Now.AddDays(cat.period);
            brw._returnDate = DateTime.MinValue;
            brw._book = bd._borrowlist.Find(x => x._barcode == bar)._book;

            bd._borrowlist.Find(x=> x._barcode == bar)._borrowDate = brw._borrowDate;
            bd._borrowlist.Find(x => x._barcode == bar)._toBeReturnedDate = brw._toBeReturnedDate;
            bd._borrowlist.Find(x => x._barcode == bar)._returnDate = brw._returnDate;

            Service.Services.BorrowerService.uppdateBorrow(brw);
            return View("Borrower", "_StandardLayout", bd);
        }
	}
}