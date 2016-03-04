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
        static private List<Author> _authorList = null;

        // Hämtar alla Authors
        static public List<Author> getAuthorList()
        {
            _authorList = new List<Author>();
            List<author> authList = AuthorRepository.dbGetAllAuthorList();
            foreach (author authObj in authList)
                _authorList.Add(MapAuthor(authObj));

            return _authorList;
        }

        // Hämtar alla Authors som börjar på angiven bokstav
        static public List<Author> getAuthorListFromFirstLetter(string c)
        {
            _authorList = new List<Author>();
            List<author> authList = AuthorRepository.dbGetAuthorListFromFirstletter(c);
            foreach (author authObj in authList)
                _authorList.Add(MapAuthor(authObj));

            return _authorList;
        }

        // Lägger till angiven Author till databasen
        static public void addAuthorToDb(Author m)
        {
            AuthorRepository.dbAddAuthor(deMapAuthor(m));  
        } 

        static private Author MapAuthor(author authObj)
        {
            Author theAuthor = new Author();
            theAuthor._id = authObj._id;
            theAuthor._firstname = authObj._firstname;
            theAuthor._lastname = authObj._lastname;
            theAuthor._birthyear = authObj._birthyear;
            return theAuthor;
        }

        static private author deMapAuthor(Author authObj)
        {
            author theAuthor = new author();
            theAuthor._id = authObj._id;
            theAuthor._firstname = authObj._firstname;
            theAuthor._lastname = authObj._lastname;
            theAuthor._birthyear = authObj._birthyear;
            return theAuthor;
        }
    }
}
