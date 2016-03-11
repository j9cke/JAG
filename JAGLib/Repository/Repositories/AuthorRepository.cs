using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public class AuthorRepository
    {
        static private List<author> dbGetAuthorList(string query)
        {
            List<author> _authorList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authorList = new List<author>();
                    while (dar.Read())
                    {
                        author authObj = new author();
                        authObj._id = (int)dar["Aid"];
                        authObj._firstname = dar["FirstName"] as string;
                        authObj._lastname = dar["LastName"] as string;
                        authObj._birthyear = dar["BirthYear"] as string;
                        _authorList.Add(authObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _authorList;
        }

        static private authordetails dbGetBooksFromAuthor(string query)
        {
            authordetails _author = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    authordetails authObj = new authordetails();
                    while (dar.Read())
                    {
                        authObj.author_firstname = dar["FirstName"] as string;
                        authObj.author_lastname = dar["LastName"] as string;
                        authObj.author_birthyear = dar["BirthYear"] as string;
                        book b = new book();
                        b._isbn = dar["ISBN"] as string;
                        b._title = dar["Title"] as string;
                        authObj._bookList.Add(b);
                    }
                    _author = authObj;
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _author;
        }

        static private author dbOneAuthorFromAid(string query)
        {
            author _author = new author();
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
                        _author._id = (int)dar["Aid"];
                        _author._firstname = dar["FirstName"] as string;
                        _author._lastname = dar["LastName"] as string;
                        _author._birthyear = dar["BirthYear"] as string;
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _author;
        }

        static private List<bookauthor> dbGetAllIsbnFromAuthor(string query)
        {
            List<bookauthor> _baList = new List<bookauthor>();
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
                        bookauthor boObj = new bookauthor();
                        boObj._aid = (int)dar["Aid"];
                        boObj._isbn = dar["ISBN"] as string;
                        _baList.Add(boObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _baList;
        }

        static private int dbCountAuthorsOnIsbn(string query)
        {
            int c = 0;
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
                        c = (int)dar["NoOf"];
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return c;
        }

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

        static public List<author> dbGetAllAuthorList()
        {
            return dbGetAuthorList("SELECT * FROM author;");
        }

        static public List<author> dbGetAuthorListFromFirstletter(string c)
        {
            return dbGetAuthorList("SELECT * FROM author WHERE LastName LIKE '" + c + "%';");
        }

        static public authordetails dbBooksFromAuthor(int aid)
        {
            return dbGetBooksFromAuthor("SELECT BOOK.ISBN, BOOK.Title, AUTHOR.FirstName, AUTHOR.LastName, AUTHOR.BirthYear, BOOK_AUTHOR.Aid FROM BOOK_AUTHOR INNER JOIN BOOK ON BOOK_AUTHOR.ISBN = dbo.BOOK.ISBN INNER JOIN AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid WHERE BOOK_AUTHOR.Aid LIKE + '" + aid + "';");
        }

        static public author dbGetAuthorFromAid(int aid)
        {
            return dbOneAuthorFromAid("SELECT * FROM AUTHOR WHERE Aid LIKE '" + aid + "';");
        }

        static public List<bookauthor> dbGetBookIsbnFromAuthor(int aid)
        {
            return dbGetAllIsbnFromAuthor("SELECT * FROM BOOK_AUTHOR WHERE Aid LIKE'" + aid + "';");
        }

        static public int dbCountAuthorsForBook(string isbn)
        {
            return dbCountAuthorsOnIsbn("SELECT COUNT(*) AS NoOf FROM BOOK_AUTHOR WHERE ISBN LIKE '" + isbn + "';");
        }

        static public void dbAddAuthor(author a) 
        {
            dbInsert("INSERT INTO AUTHOR (FirstName, LastName, BirthYear) VALUES ('" + a._firstname + "', '" + a._lastname + "', '" + a._birthyear + "');");
        }

        static public void dbRemoveAuthor(int aid)
        {
            dbRemoveOrEdit("DELETE FROM AUTHOR WHERE Aid LIKE '" + aid + "';");
        }

        static public void dbRemoveBookAuthor(int aid)
        {
            dbRemoveOrEdit("DELETE FROM BOOK_AUTHOR WHERE Aid LIKE '" + aid + "';");
        }

        static public void dbRemoveBookAuthor(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM BOOK_AUTHOR WHERE ISBN LIKE '" + isbn + "';");
        }

        static public void dbEditAuthor(author a)
        {
            dbRemoveOrEdit("UPDATE AUTHOR SET FirstName='" + a._firstname + "', LastName='" + a._lastname + "', BirthYear='" + a._birthyear + "' WHERE Aid='" + a._id + "';");
        }
    }
}