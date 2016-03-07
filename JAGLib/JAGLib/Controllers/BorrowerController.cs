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
                Borrower model = Service.Services.BorrowerService.getBorrower(pId);
                Session["name"] = model._firstname + " " + model._lastname; 
             
                    
                return View("Borrower", "_StandardLayout", model);
            }
        }
	}
}