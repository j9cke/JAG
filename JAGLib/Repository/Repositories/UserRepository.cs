using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public class UserRepository
    {
        static public logindata dbGetUser(string id)
        {
            logindata _userObj = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM borrower WHERE PersonId = " + id + ";", con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.Read())
                {
                    _userObj = new logindata();
                    _userObj._username = dar["PersonId"] as string;
                    _userObj._password = dar["Password"] as string;
                    _userObj._salt = dar["Salt"] as string;
                    _userObj._level = dar["Level"] as string;
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
            return _userObj;
        }

        static private List<logindata> dbGetUserList(string query)
        {
            List<logindata> _userList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _userList = new List<logindata>();
                    while (dar.Read())
                    {
                        logindata _userObj = new logindata();
                        _userObj._username = dar["PersonId"] as string;
                        _userObj._password = dar["Password"] as string;
                        _userObj._salt = dar["Salt"] as string;
                        _userObj._level = dar["Level"] as string;
                        _userList.Add(_userObj);
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
            return _userList;
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


        static public List<logindata> dbGetAllUserList()
        {
            return dbGetUserList("SELECT * FROM login;");
        }


        

        static public void dbAddUser(logindata b)
        {
            dbInsert("INSERT INTO LOGIN (PersonId, Password, Salt, Level) VALUES ('" + b._username + "', '" + b._password+ "', '" + b._salt + "', '" + b._level + "');");
        }
        //static public List<logindata> dbGetDepartmentEmployeeList(int catId)
        //{
        //    return dbGetUserList("SELECT * FROM login WHERE id = " + Convert.ToString(catId) + ";");
        //}

    }
}