﻿using System;
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
            if (_borrowerList == null)
            {
                _borrowerList = new List<Borrower>();
                List<borrower> brwList = BorrowerRepository.dbGetAllBorrowerList();
                foreach (borrower brwObj in brwList)
                {
                    _borrowerList.Add(MapBorrower(brwObj));
                }
            }
            return _borrowerList;
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
    }
}