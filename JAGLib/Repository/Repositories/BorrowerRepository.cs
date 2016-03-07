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
        static public borrower dbGetBorrower(string id)
        {
            borrower _brwObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM borrower WHERE PersonId = '" + id + "';", con);
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
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return _brwObj;
        }

        static private List<borrower> dbGetBorrowerList(string query)
        {
            List<borrower> _brwList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
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
            catch (Exception eObj)
            {
                throw eObj;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return _brwList;
        }

        static public List<borrower> dbGetAllBorrowerList()
        {
            return dbGetBorrowerList("SELECT * FROM borrower;");
        }

        static public List<borrower> dbGetDepartmentEmployeeList(int catId)
        {
            return dbGetBorrowerList("SELECT * FROM borrower WHERE aid = " + Convert.ToString(catId) + ";");
        }

    }
}