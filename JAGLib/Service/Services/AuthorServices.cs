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

        // Hämtar alla böcker som en Author har skrivit
        static public AuthorDetails getBooksFromAuthor(int aid) 
        {
            authordetails authObj = AuthorRepository.dbBooksFromAuthor(aid);

            return MapAuthorDetails(authObj);
        }

        // Hämtar en author på Aid
        static public Author getAuthorFromAid(int aid)
        {
            author authObj = AuthorRepository.dbGetAuthorFromAid(aid);

            return MapAuthor(authObj);
        }

        // Lägger till angiven Author till databasen
        static public void addAuthorToDb(Author m)
        {
            AuthorRepository.dbAddAuthor(deMapAuthor(m));  
        } 

        // Tar bort en author på Aid & eventuellt hans böcker
        static public void Remove(int aid)
        {
            List<string> removeThisBooks = BooksThatShallBeRemoves(aid);

            AuthorRepository.dbRemoveBookAuthor(aid);       // Ta bort author ur book_author
            AuthorRepository.dbRemoveAuthor(aid);           // Ta bort authorn

            if (removeThisBooks.Count() > 0) 
                foreach (string isbn in removeThisBooks)
                {
                    BookRepository.dbRemoveCopies(isbn);
                    BookRepository.dbRemoveBook(isbn);    // Ta bort böcker som skrivits av endast hen
                }
        }

        // Listar de böcker som skall tas bort, alltså de böcker som skrivits endast av author 
        static private List<string> BooksThatShallBeRemoves(int aid)
        {
            List<string> bookIsbnsToRemove = new List<string>();
            
            List<bookauthor> baList = AuthorRepository.dbGetBookIsbnFromAuthor(aid); //alla Authors böcker
            foreach (bookauthor b in baList)
            {
                int noOfAuthors = AuthorRepository.dbCountAuthorsForBook(b._isbn);

                if (noOfAuthors == 1)
                    bookIsbnsToRemove.Add(b._isbn);
            }

            return bookIsbnsToRemove;
        }

        // Editera author med värdena som kommer in
        static public void EditAuthor(Author a)
        {
            AuthorRepository.dbEditAuthor(deMapAuthor(a));
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

        static private AuthorDetails MapAuthorDetails(authordetails authObj)
        {
            AuthorDetails theAuthorDetails = new AuthorDetails();
            theAuthorDetails.author_firstname = authObj.author_firstname;
            theAuthorDetails.author_lastname = authObj.author_lastname;
            theAuthorDetails.author_birthyear = authObj.author_birthyear;

            foreach (book bObj in authObj._bookList)
            {
                Book book = new Book();
                book._isbn = bObj._isbn;
                book._title = bObj._title;
                theAuthorDetails._bookList.Add(book);
            }

            return theAuthorDetails;
        }
    }
}
