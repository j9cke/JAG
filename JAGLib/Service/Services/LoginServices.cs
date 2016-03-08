using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Repository.Repositories;
using Repository.EntityModel;


namespace Service.Services
{
    public class LoginService
    {
        static private List<LoginData> _userList = null;

        static public List<LoginData> getUserList()
        {
            if (_userList == null)
            {
                _userList = new List<LoginData>();
                List<logindata> usrList = UserRepository.dbGetAllUserList();
                foreach (logindata userObj in usrList)
                {
                    _userList.Add(MapBorrower(userObj));
                }
            }
            return _userList;
        }


        static private LoginData MapBorrower(logindata userObj)
        {
            LoginData theUser = new LoginData();
            theUser._username = userObj._username;
            theUser._password = userObj._password;
            theUser._salt = userObj._salt;
            theUser._level = userObj._level;
            return theUser;
        }

        static private logindata deMapUser(LoginData userObj)
        {
            logindata theUser = new logindata();
            theUser._username = userObj._username;
            theUser._password = userObj._password;
            theUser._salt = userObj._salt;
            theUser._level = userObj._level;
            return theUser;
        }

        static public void addUserToDb(LoginData m)
        {
            UserRepository.dbAddUser(deMapUser(m));
        } 
    }
}