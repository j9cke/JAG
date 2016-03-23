using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public class BookRepository
    {
        // Används om vi bara vill ha böcker
        static private List<book> dbGetBookList(string query, SqlParameter[] sp)
        {
            List<book> _bookList = null;
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
                    _bookList = new List<book>();
                    while (dar.Read())
                    {
                        book bookObj = new book();
                        bookObj._isbn = dar["ISBN"] as string;
                        bookObj._title = dar["Title"] as string;
                        bookObj._signId = (int)dar["SignId"];
                        bookObj._publicationYear = dar["PublicationYear"] as string;
                        bookObj._publicationInfo = dar["publicationinfo"] as string;
                        bookObj._pages = (Int16)dar["pages"];
                        _bookList.Add(bookObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _bookList;
        }

        // Om vi vill ha en bok tillsammans med author & annan info
        static private List<bookdetails> dbGetBookDetails(string query, SqlParameter[] sp)
        {
            List<bookdetails> _bdetailsList = null;
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
                    _bdetailsList = new List<bookdetails>();
                    while (dar.Read())
                    {
                        bookdetails bkdtObj = new bookdetails();
                        bkdtObj.book_isbn = dar["ISBN"] as string;
                        bkdtObj.book_title = dar["Title"] as string;
                        bkdtObj.book_signId = (int)dar["SignId"];
                        bkdtObj.book_publicationYear = dar["PublicationYear"] as string;
                        bkdtObj.book_publicationInfo = dar["publicationinfo"] as string;
                        bkdtObj.book_pages = (Int16)dar["pages"];
                        bkdtObj.author_firstname = dar["FirstName"] as string;
                        bkdtObj.author_lastname = dar["LastName"] as string;
                        _bdetailsList.Add(bkdtObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _bdetailsList;
        }

        // Hämta en bok från Databasen på ISBN
        static private book dbBookFromISBN(string query, SqlParameter[] sp)
        {
            book _book = null;
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
                        book bookObj = new book();
                        bookObj._isbn = dar["ISBN"] as string;
                        bookObj._title = dar["Title"] as string;
                        bookObj._signId = (int)dar["SignId"];
                        bookObj._publicationYear = dar["PublicationYear"] as string;
                        bookObj._publicationInfo = dar["publicationinfo"] as string;
                        bookObj._pages = (Int16)dar["pages"];
                        _book = bookObj;
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _book;
        }

        static private List<copy> dbGetCopy(string query, SqlParameter[] sp)
        {
            List<copy> _copy = new List<copy>();
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
                        copy copyObj = new copy();
                        copyObj.copy_isbn = dar["ISBN"] as string;
                        copyObj.copy_barcode = dar["Barcode"] as string;
                        copyObj.copy_library = dar["library"] as string;
                        copyObj.copy_status = (int)dar["StatusId"];
                        copyObj.copy_location = dar["Location"] as string;
                        _copy.Add(copyObj);
                    }
                }
            }
            catch (Exception cObj) { throw cObj; }
            finally { if (con != null) con.Close(); }

            return _copy;
        }

        // Används för att skicka in saker i databasen
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

        static private bool dbHaveLoans(string query, SqlParameter[] sp)
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

        static private string dbLastBarcode(string query, SqlParameter[] sp)
        {
            string bc = "";
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
                    bc = dar["bc"] as string;
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return bc;
        }

        static public List<book> dbGetAllBookList()
        {
            return dbGetBookList("SELECT * FROM BOOK;", null);
        }

        static public List<book> dbGetBookListOnFirstLetter(string c)
        {
            return dbGetBookList("SELECT * FROM BOOK WHERE Title LIKE @TITLE + '%' ORDER BY Title;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@TITLE",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c
                }
            });
        }

        static public List<bookdetails> dbGetDetailsOfBook(string isbn)
        {
            return dbGetBookDetails("SELECT BOOK.ISBN, BOOK.Title, BOOK.SignId, BOOK.PublicationYear, BOOK.publicationinfo, BOOK.pages, AUTHOR.FirstName, AUTHOR.LastName FROM DBLibrary.dbo.BOOK INNER JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN INNER JOIN AUTHOR ON BOOK_AUTHOR.Aid = AUTHOR.Aid WHERE BOOK.ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public book dbGetBookFromISBN(string isbn)
        {
            return dbBookFromISBN("SELECT * FROM BOOK WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public List<copy> dbGetCopyFromISBN(string isbn)
        {
            return dbGetCopy("SELECT * FROM COPY WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public void dbAddBook(book b)
        {
            dbInsert("INSERT INTO BOOK (ISBN, Title, SignId, PublicationYear, publicationinfo, pages) VALUES (@ISBN, @TITLE, @SIGNID, @PUBYEAR, @PUBINFO, @PAGES);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._isbn
                },
                new SqlParameter() {
                    ParameterName = "@TITLE",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._title
                },
                new SqlParameter() {
                    ParameterName = "@SIGNID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = b._signId
                },
                new SqlParameter() {
                    ParameterName = "@PUBYEAR",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._publicationYear
                },
                new SqlParameter() {
                    ParameterName = "@PUBINFO",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._publicationInfo
                },
                new SqlParameter() {
                    ParameterName = "@PAGES",
                    SqlDbType = System.Data.SqlDbType.SmallInt,
                    Value = b._pages
                }
            });
        }

        static public void dbRemoveBook(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM BOOK WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public void dbRemoveCopies(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM COPY WHERE ISBN = @ISBN;;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public void dbRemoveBorrows(string bc)
        {
            dbRemoveOrEdit("DELETE FROM BORROW WHERE BarCode = @BARC;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@BARC",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = bc
                }
            });
        }

        static public void dbEditBook(book b)
        {
            dbRemoveOrEdit("UPDATE BOOK SET Title=@TITLE, SignId=@SIGNID, PublicationYear=@PUBYEAR, publicationinfo=@PUBINFO, pages=@PAGES WHERE ISBN=@ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._isbn
                },
                new SqlParameter() {
                    ParameterName = "@TITLE",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._title
                },
                new SqlParameter() {
                    ParameterName = "@SIGNID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = b._signId
                },
                new SqlParameter() {
                    ParameterName = "@PUBYEAR",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._publicationYear
                },
                new SqlParameter() {
                    ParameterName = "@PUBINFO",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._publicationInfo
                },
                new SqlParameter() {
                    ParameterName = "@PAGES",
                    SqlDbType = System.Data.SqlDbType.SmallInt,
                    Value = b._pages
                }
            });
        }

        static public bool dbHaveCopysOnLoan(string isbn) {
            return dbHaveLoans("SELECT COUNT(*) AS No FROM COPY INNER JOIN BORROW ON COPY.Barcode = BORROW.Barcode WHERE ISBN = @ISBN AND ReturnDate IS NULL;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public string dbGetLastBarcode() {
            return dbLastBarcode("SELECT MAX(BARCODE) AS bc FROM COPY;", null);
        }

        static public void dbAddCopy(copy c)
        {
            dbInsert("INSERT INTO COPY VALUES(@BARC, @LOC, @STAT, @ISBN, @LIB);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@BARC",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c.copy_barcode
                },
                new SqlParameter() {
                    ParameterName = "@LOC",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c.copy_location
                },
                new SqlParameter() {
                    ParameterName = "@STAT",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = c.copy_status
                },
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c.copy_isbn
                },
                new SqlParameter() {
                    ParameterName = "@LIB",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c.copy_library
                }
            });
        }

        static public List<book> dbSearchBook(string s)
        {
            return dbGetBookList("SELECT * FROM BOOK WHERE BOOK.Title LIKE '%' + @SEARCH + '%';", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@SEARCH",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = s
                }
            });
        }
    }
}