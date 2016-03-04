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
        static public author dbGetEmployee(int id)
        {
            author _empObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM author WHERE aid = " + Convert.ToString(id) + ";", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _empObj = new author();
                    _empObj._id = (int)dar["Aid"];
                    _empObj._firstname = dar["FirstName"] as string;
                    _empObj._lastname = dar["LastName"] as string;
                    _empObj._birthyear = dar["BirthYear"] as string;
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return _empObj;
        }

        static private List<author> dbGetEmployeeList(string query)
        {
            List<author> _empList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _empList = new List<author>();
                    while (dar.Read())
                    {
                        author empObj = new author();
                        empObj._id = (int)dar["Aid"];
                        empObj._firstname = dar["FirstName"] as string;
                        empObj._lastname = dar["LastName"] as string;
                        empObj._birthyear = dar["BirthYear"] as string;
                        _empList.Add(empObj);
                    }
                }
            }
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return _empList;
        }

        static public List<author> dbGetAllEmployeeList()
        {
            return dbGetEmployeeList("SELECT * FROM author;");
        }

        static public List<author> dbGetDepartmentEmployeeList(int depId)
        {
            return dbGetEmployeeList("SELECT * FROM author WHERE aid = " + Convert.ToString(depId) + ";");
        }

    }
}