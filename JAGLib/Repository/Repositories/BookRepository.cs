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

        static public List<book> dbGetAllBookList()
        {
            return dbGetBookList("SELECT * FROM BOOK;");
        }

        static public List<book> dbGetBookListOnFirstLetter(string c)
        {
            return dbGetBookList("SELECT * FROM BOOK WHERE Title LIKE '" + c + "%';");
        }
    }
}