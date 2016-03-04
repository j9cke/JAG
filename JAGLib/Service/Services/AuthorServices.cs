using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Repository.Repositories;
using Repository.EntityModel;

namespace Service.Services
{
    public class AuthorServices
    {
        static private List<Author> _employeeList = null;

        static public List<Author> getEmployeeList()
        {
            if (_employeeList == null)
            {
                _employeeList = new List<Author>();
                List<author> empList = AuthorRepository.dbGetAllEmployeeList();
                foreach (author empObj in empList)
                {
                    _employeeList.Add(MapEmployee(empObj));
                }
            }
            return _employeeList;
        }

        static public List<Author> getEmployeeList(int id)
        {
            if (_employeeList == null)
            {
                _employeeList = new List<Author>();
                List<author> empList = AuthorRepository.dbGetDepartmentEmployeeList(id);
                foreach (author empObj in empList)
                {
                    _employeeList.Add(MapEmployee(empObj));
                }
            }
            return _employeeList;
        }

        static private Author MapEmployee(author empObj)
        {
            Author theAuthor = new Author();
            theAuthor._id = empObj._id;
            theAuthor._firstname = empObj._firstname;
            theAuthor._lastname = empObj._lastname;
            theAuthor._birthyear = empObj._birthyear;
            return theAuthor;
        }
    }
}
