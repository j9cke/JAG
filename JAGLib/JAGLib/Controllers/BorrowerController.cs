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
            if (Session["pid"] == null)
                return Redirect("/Home/Login/");
            else {
                //hämta borrower från SQL
                var model = new Borrower();
                string pId = Session["pId"].ToString();

                for (int i = 0; i < mup.borrowerList.Count(); i++)
                {
                    if (pId == mup.borrowerList[i]._pid)
                    {
                        model._pid = mup.borrowerList[i]._pid;
                        model._firstname = mup.borrowerList[i]._firstname;
                        model._lastname = mup.borrowerList[i]._lastname;
                        model._phoneno = mup.borrowerList[i]._phoneno;
                        model._catId = mup.borrowerList[i]._catId;
                        Session["name"] = model._firstname + ' ' + model._lastname;
                    }
                }
                    
                return View("Borrower", "_StandardLayout", model);
            }
        }
	}
}