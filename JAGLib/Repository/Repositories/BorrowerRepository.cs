using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public class BorrowerRepository
    {
        static private borrower dbGetBorrower(string query, SqlParameter[] sp)
        {
            borrower _brwObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _brwObj = new borrower();
                    _brwObj._pid = dar["PersonId"] as string;
                    _brwObj._firstname = dar["FirstName"] as string;
                    _brwObj._lastname = dar["LastName"] as string;
                    _brwObj._address = dar["Address"] as string;
                    _brwObj._phoneno = dar["Telno"] as string;
                    _brwObj._catId = (int)dar["CategoryId"];
                }
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _brwObj;
        }

        static private List<borrower> dbGetBorrowerList(string query, SqlParameter[] sp)
        {
            List<borrower> _brwList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _brwList = new List<borrower>();
                    while (dar.Read())
                    {
                        borrower _brwObj = new borrower();
                        _brwObj._pid = dar["PersonId"] as string;
                        _brwObj._firstname = dar["FirstName"] as string;
                        _brwObj._lastname = dar["LastName"] as string;
                        _brwObj._address = dar["Address"] as string;
                        _brwObj._phoneno = dar["Telno"] as string;
                        string cat = dar["CategoryId"] as string;
                        if (cat == null)
                            _brwObj._catId = 0;
                        else
                            _brwObj._catId = Convert.ToInt32(cat);
                        _brwList.Add(_brwObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _brwList;
        }

        static private void dbInsert(string query, SqlParameter[] sp)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static private void dbRemoveOrEdit(string query, SqlParameter[] sp)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static private category dbGetCategoryforCatId(string query, SqlParameter[] sp)
        {
            category cat = new category();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    cat.categoryId = (int)dar["CatergoryId"]; // as string;
                    cat.period = Convert.ToInt32(dar["Period"]); //as string;
                    cat.categoryt = dar["Category"] as string;
                    cat.penaltyperday = (int)dar["Penaltyperday"];
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return cat;
        }

        static private bool dbHaveBorrows(string query, SqlParameter[] sp)
        {
            int count = 0;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    count = (int)dar["No"];
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            if (count > 0)
                return true;
            else 
                return false;
        }

        static private borrowerdetails dbGetBorrowerDetailsforpid(string query, SqlParameter[] sp)
        {
            borrowerdetails brwd = new borrowerdetails();;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        brwd._pid = dar["PersonId"] as string;
                        brwd._firstname = dar["FirstName"] as string;
                        brwd._lastname = dar["LastName"] as string;
                        brwd._address = dar["Address"] as string;
                        brwd._phoneno = dar["Telno"] as string;
                        brwd._catId = (int)dar["CategoryId"];
                        
                        borrow _brwObj = new borrow();
                        _brwObj.barcode = dar["Barcode"] as string;

                        DateTime temp;
                        if (DateTime.TryParse(dar["ReturnDate"].ToString(), out temp))
                            _brwObj.returnDate = temp;
                        if (DateTime.TryParse(dar["BorrowDate"].ToString(), out temp))
                            _brwObj.borrowDate = temp;
                        if (DateTime.TryParse(dar["ToBeReturnedDate"].ToString(), out temp))
                            _brwObj.toBeReturnedDate = temp;


                        if (_brwObj.returnDate == DateTime.MinValue && _brwObj.toBeReturnedDate < DateTime.Now)
                        {
                            category cat = dbGetCategory(brwd._catId.ToString());
                            _brwObj.penalty = ((DateTime.Today - _brwObj.toBeReturnedDate.Date).TotalDays * cat.penaltyperday).ToString();
                        }
                        else if(_brwObj.returnDate >  _brwObj.toBeReturnedDate)
                        {
                            category cat = dbGetCategory(brwd._catId.ToString());
                            _brwObj.penalty = ((_brwObj.returnDate - _brwObj.toBeReturnedDate.Date).TotalDays * cat.penaltyperday).ToString();
                        }
                       
                        else
                        _brwObj.penalty = "0";


                         _brwObj.pid = dar["PersonId"] as string;

                        _brwObj.book = new book();
                        _brwObj.book._isbn = dar["ISBN"] as string;
                        _brwObj.book._title = dar["Title"] as string;
                        _brwObj.book._signId = (int)dar["SignId"];
                        _brwObj.book._publicationYear = dar["PublicationYear"] as string;
                        _brwObj.book._publicationInfo = dar["publicationinfo"] as string;
                        _brwObj.book._pages = (Int16)dar["pages"];
  
                        brwd._borrowlist.Add(_brwObj);
                    }
                }
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return brwd;
        }

        static public List<borrower> dbGetAllBorrowerList()
        {
            return dbGetBorrowerList("SELECT * FROM borrower;", null);
        }

        static public void dbAddBorrower(borrower b)
        {
            dbInsert("INSERT INTO BORROWER VALUES (@ID, @FIRST, @LAST, @ADDRESS, @PHONE, @CATID);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._pid
                },
                new SqlParameter() {
                    ParameterName = "@FIRST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._firstname
                },
                new SqlParameter() {
                    ParameterName = "@LAST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._lastname
                },
                new SqlParameter() {
                    ParameterName = "@ADDRESS",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._address
                },
                new SqlParameter() {
                    ParameterName = "@PHONE",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._phoneno
                },
                new SqlParameter() {
                    ParameterName = "@CATID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = b._catId
                }
            });
        }

        static public borrowerdetails dbGetBorrowerDetails(string pid)
        {
            return dbGetBorrowerDetailsforpid("SELECT * FROM BORROW INNER JOIN BORROWER ON BORROW.PersonId = BORROWER.PersonId INNER JOIN COPY ON BORROW.Barcode = COPY.Barcode INNER JOIN BOOK ON COPY.ISBN = BOOK.ISBN WHERE BORROWER.PersonId = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public borrower getBorrower(string pid)
        {
            return dbGetBorrower("SELECT * FROM borrower WHERE PersonId = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public bool dbDoesHeHaveBorrows(string pid)
        {
            return dbHaveBorrows("SELECT COUNT(*) AS No FROM BORROWER INNER JOIN BORROW ON BORROWER.PersonId = BORROW.PersonId WHERE BORROWER.PersonId = @ID AND ReturnDate IS NULL;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public void dbRemoveBorrower(string pid)
        {
            dbRemoveOrEdit("DELETE FROM BORROWER WHERE PersonId = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public void dbRemoveBorrows(string pid)
        {
            dbRemoveOrEdit("DELETE FROM BORROW WHERE PersonId = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public void dbRemoveLogin(string pid)
        {
            dbRemoveOrEdit("DELETE FROM LOGIN WHERE PersonId = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = pid
                }
            });
        }

        static public void dbEditBorrower(borrower b)
        {
            dbRemoveOrEdit("UPDATE BORROWER SET FirstName=@FIRST, LastName=@LAST, Address=@ADDRESS, Telno=@PHONE, CategoryId=@CATID WHERE PersonId=@ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._pid
                },
                new SqlParameter() {
                    ParameterName = "@FIRST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._firstname
                },
                new SqlParameter() {
                    ParameterName = "@LAST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._lastname
                },
                new SqlParameter() {
                    ParameterName = "@ADDRESS",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._address
                },
                new SqlParameter() {
                    ParameterName = "@PHONE",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._phoneno
                },
                new SqlParameter() {
                    ParameterName = "@CATID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = b._catId
                }
            });
        }

        static public void dbUppdateBorrow(borrow b)
        {
            if (b.returnDate == DateTime.MinValue)
                dbRemoveOrEdit("UPDATE BORROW SET BorrowDate=@BD, ToBeReturnedDate=@TBRD, ReturnDate = NULL WHERE Barcode=@BC;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@BD",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = b.borrowDate
                },
                new SqlParameter() {
                    ParameterName = "@TBRD",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = b.toBeReturnedDate
                },
                new SqlParameter() {
                    ParameterName = "@BC",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b.barcode
                }
            });
        
            else
                dbRemoveOrEdit("UPDATE BORROW SET BorrowDate=@BD, ToBeReturnedDate=@TBRD, ReturnDate=@RD WHERE Barcode=@BC;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@BD",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = b.borrowDate
                },
                new SqlParameter() {
                    ParameterName = "@TBRD",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = b.toBeReturnedDate
                },
                new SqlParameter() {
                    ParameterName = "@RD",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = b.returnDate
                },
                new SqlParameter() {
                    ParameterName = "@BC",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b.barcode
                }
            });
        }
        
        static public category dbGetCategory(string catID)
        {
            return dbGetCategoryforCatId("SELECT * FROM CATEGORY WHERE CatergoryId = @CATID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@CATID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = catID
                }
            });
        }
   }
}