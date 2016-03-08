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


                    
                return View("Borrower", "_StandardLayout", model);
            }
        }
	}
}