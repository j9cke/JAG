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
        static private List<logindata> dbGetUserList(string query, SqlParameter[] sp)
        {
            List<logindata> _userList = null;
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

            catch (Exception eObj) { throw eObj; }
            finally
            { if (con != null) con.Close(); }

            return _userList;
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

        static public List<logindata> dbGetAllUserList()
        {
            return dbGetUserList("SELECT * FROM login;", null);
        }

        static public void dbAddUser(logindata b)
        {
            dbInsert("INSERT INTO LOGIN (PersonId, Password, Salt, Level) VALUES (@USER, @PW, @SALT, @LEVEL);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@USER",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._username
                },
                new SqlParameter() {
                    ParameterName = "@PW",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._password
                },
                new SqlParameter() {
                    ParameterName = "@SALT",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._salt
                },
                new SqlParameter() {
                    ParameterName = "@LEVEL",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = b._level
                }
            });
        }
    }
}