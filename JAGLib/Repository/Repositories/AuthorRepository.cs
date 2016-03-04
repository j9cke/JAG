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

        static public List<author> dbGetAllAuthorList()
        {
            return dbGetAuthorList("SELECT * FROM author;");
        }

        static public List<author> dbGetAuthorListFromFirstletter(string c)
        {
            return dbGetAuthorList("SELECT * FROM author WHERE LastName LIKE '" + c + "%';");
        }

        static public void dbAddAuthor(author a) 
        {
            dbInsert("INSERT INTO AUTHOR (FirstName, LastName, BirthYear) VALUES ('" + a._firstname + "', '" + a._lastname + "', '" + a._birthyear + "');");
        }
    }
}