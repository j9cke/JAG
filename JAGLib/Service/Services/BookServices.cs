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
        static private List<BookDetails> _bkdtList = null;

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

        static public List<BookDetails> getBookDetailsFromIsbn(string isbn)
        {
            _bkdtList = new List<BookDetails>();
            List<bookdetails> bList = BookRepository.dbGetDetailsOfBook(isbn);

            foreach (bookdetails item in bList)
                _bkdtList.Add(MapBookDetails(item));

            return _bkdtList;
        }

        static public Book getBookFromISBN(string isbn)
        {
            book bookObj = BookRepository.dbGetBookFromISBN(isbn);

            return MapBook(bookObj);
        }

        // Lägger till angiven Author till databasen
        static public void addBookToDb(Book m)
        {
            BookRepository.dbAddBook(deMapBook(m));
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

        static private book deMapBook(Book bookObj)
        {
            book theBook = new book();
            theBook._isbn = bookObj._isbn;
            theBook._title = bookObj._title;
            theBook._signId = bookObj._signId;
            theBook._publicationYear = bookObj._publicationYear;
            theBook._publicationInfo = bookObj._publicationInfo;
            theBook._pages = bookObj._pages;
            return theBook;
        }

        static private BookDetails MapBookDetails(bookdetails bkdtObj)
        {
            BookDetails theBookDts = new BookDetails();
            theBookDts.book_isbn = bkdtObj.book_isbn;
            theBookDts.book_title = bkdtObj.book_title;
            theBookDts.book_signId = bkdtObj.book_signId;
            theBookDts.book_publicationYear = bkdtObj.book_publicationYear;
            theBookDts.book_publicationInfo = bkdtObj.book_publicationInfo;
            theBookDts.book_pages = bkdtObj.book_pages;
            theBookDts.author_firstname = bkdtObj.author_firstname;
            theBookDts.author_lastname = bkdtObj.author_lastname;
            return theBookDts;
        }
    }
}
