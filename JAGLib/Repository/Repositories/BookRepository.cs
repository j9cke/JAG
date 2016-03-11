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
        static private List<book> dbGetBookList(string query)
        {
            List<book> _bookList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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
        static private List<bookdetails> dbGetBookDetails(string query)
        {
            List<bookdetails> _bdetailsList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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
        static private book dbBookFromISBN(string query)
        {
            book _book = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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

        // Används för att skicka in saker i databasen
        static private void dbInsert(string query)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static private void dbRemoveOrEdit(string query)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static public List<book> dbGetAllBookList()
        {
            return dbGetBookList("SELECT * FROM BOOK;");
        }

        static public List<book> dbGetBookListOnFirstLetter(string c)
        {
            return dbGetBookList("SELECT * FROM BOOK WHERE Title LIKE '" + c + "%';");
        }

        static public List<bookdetails> dbGetDetailsOfBook(string isbn)
        {
            return dbGetBookDetails("SELECT BOOK.ISBN, BOOK.Title, BOOK.SignId, BOOK.PublicationYear, BOOK.publicationinfo, BOOK.pages, AUTHOR.FirstName, AUTHOR.LastName FROM DBLibrary.dbo.BOOK INNER JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN INNER JOIN AUTHOR ON BOOK_AUTHOR.Aid = AUTHOR.Aid WHERE BOOK.ISBN LIKE '" + isbn + "' ;");
        }

        static public book dbGetBookFromISBN(string isbn)
        {
            return dbBookFromISBN("SELECT * FROM BOOK WHERE ISBN LIKE '" + isbn + "';");
        }

        static public void dbAddBook(book b)
        {
            dbInsert("INSERT INTO BOOK (ISBN, Title, SignId, PublicationYear, publicationinfo, pages) VALUES ('" + b._isbn + "', '" + b._title + "', '" + b._signId + "', '" + b._publicationYear + "', '" + b._publicationInfo + "', '" + b._pages + "');");
        }

        static public void dbRemoveBook(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM BOOK WHERE ISBN LIKE '" + isbn + "';");
        }

        static public void dbRemoveCopies(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM COPY WHERE ISBN LIKE '" + isbn + "';");
        }

        static public void dbEditBook(book b)
        {
            dbRemoveOrEdit("UPDATE BOOK SET Title='" + b._title + "', SignId='" + b._signId + "', PublicationYear='" + b._publicationYear + "', publicationinfo='" + b._publicationInfo + "', pages='" + b._pages + "' WHERE ISBN='" + b._isbn + "';");
        }
    }
}