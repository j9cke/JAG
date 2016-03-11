using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Repository.Repositories;
using Repository.EntityModel;


namespace Service.Services
{
    public class BorrowerService
    {
        static private List<Borrower> _borrowerList = null;

        static public List<Borrower> getBorrowerList()
        {
            _borrowerList = new List<Borrower>();
            List<borrower> brwList = BorrowerRepository.dbGetAllBorrowerList();
            foreach (borrower brwObj in brwList)
            {
                _borrowerList.Add(MapBorrower(brwObj));
            }
    
            return _borrowerList;
        }
       
        static public Borrower getBorrower(string id)
        {
            return MapBorrower(BorrowerRepository.dbGetBorrower(id));
        }

        // Tar bort en Borrower på PersonId & borrowerns lån
        static public void Remove(string pid)
        {
            BorrowerRepository.dbRemoveBorrows(pid);    // Ta bort lån
            BorrowerRepository.dbRemoveLogin(pid);      // Ta bort logindata
            BorrowerRepository.dbRemoveBorrower(pid);   // Ta bort borrowern
        }

        static private Borrower MapBorrower(borrower brwObj)
        {
            Borrower theBorrower = new Borrower();
            theBorrower._pid = brwObj._pid;
            theBorrower._firstname = brwObj._firstname;
            theBorrower._lastname = brwObj._lastname;
            theBorrower._address = brwObj._address;
            theBorrower._phoneno = brwObj._phoneno;
            theBorrower._catId = brwObj._catId;
            theBorrower._password = "";
            return theBorrower;
        }

        static private borrower deMapBorrower(Borrower brwObj)
        {
            borrower theBorrower = new borrower();
            theBorrower._pid = brwObj._pid;
            theBorrower._firstname = brwObj._firstname;
            theBorrower._lastname = brwObj._lastname;
            theBorrower._address = brwObj._address;
            theBorrower._phoneno = brwObj._phoneno;
            theBorrower._catId = Convert.ToInt32(brwObj._catId);
            return theBorrower;
        }

        static public void addBorrowerToDb(Borrower m)
        {
            BorrowerRepository.dbAddBorrower(deMapBorrower(m));
        }

        // Editera author med värdena som kommer in
        static public void EditBorrower(Borrower b)
        {
            BorrowerRepository.dbEditBorrower(deMapBorrower(b));
        }

        /************************* GET BORROW FOR A PERSON   ***********************/
        static private Borrow MapBorrow(borrow brwObj)
        {
            Borrow theBorrow = new Borrow();
            theBorrow._barcode = brwObj.barcode;
            theBorrow._pid = brwObj.pid;
            theBorrow._borrowDate = brwObj.borrowDate;
            theBorrow._returnDate = brwObj.returnDate;
            theBorrow._toBeReturnedDate = brwObj.toBeReturnedDate;
            theBorrow._book = Service.Services.BookServices.MapBookPublic(brwObj.book);
            return theBorrow;
        }


        static public List<Borrow> getPersonsBorrowList(Borrower borrower)
        {
           List<Borrow> borrowList = new List<Borrow>();
            
           List<borrow> brwList = BorrowerRepository.dbGetAllBorrowList(borrower._pid);
           foreach (borrow brwObj in brwList)
           {
               borrowList.Add(MapBorrow(brwObj));
           }
            
           return borrowList;
        }

        static private BorrowerDetails MapBorrowerDetails(borrowerdetails brwObj)
        {
            BorrowerDetails theBD = new BorrowerDetails();
            theBD._pid = brwObj._pid;
            theBD._firstname = brwObj._firstname;
            theBD._lastname = brwObj._lastname;
            theBD._address = brwObj._address;
            theBD._phoneno = brwObj._phoneno;
            theBD._catId = brwObj._catId;
            foreach(borrow borrow in brwObj._borrowlist)
            {
                theBD._borrowlist.Add(MapBorrow(borrow));
            }
           
            return theBD;
        }

        static public BorrowerDetails getBorrowerDetails(string pid)
        {
            return MapBorrowerDetails(BorrowerRepository.dbGetBorrowerDetails(pid));
        }
    }
}