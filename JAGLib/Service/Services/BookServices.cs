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
    public class BookServices
    {
        static private List<Book> _bookList = null;

        // Hämtar alla böcker
        static public List<Book> getBookList()
        {
            _bookList = new List<Book>();
            List<book> bList = BookRepository.dbGetAllBookList();

            foreach (book bookObj in bList)
                _bookList.Add(MapBook(bookObj));

            return _bookList;
        }

        // Hämtar böcker som börjar på angiven bokstav
        static public List<Book> getBookListOnFirstLetter(string c)
        {
            _bookList = new List<Book>();
            List<book> bList = BookRepository.dbGetBookListOnFirstLetter(c);

            foreach (book bookObj in bList)
                _bookList.Add(MapBook(bookObj));

            return _bookList;
        }

        static private Book MapBook(book bookObj)
        {
            Book theBook = new Book();
            theBook._isbn = bookObj._isbn;
            theBook._title = bookObj._title;
            theBook._signId = bookObj._signId;
            theBook._publicationYear = bookObj._publicationYear;
            theBook._publicationInfo = bookObj._publicationInfo;
            theBook._pages = bookObj._pages;
            return theBook;
        }
    }
}
