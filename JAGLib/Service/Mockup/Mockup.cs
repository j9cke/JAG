using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Models;

namespace Service.Mockup
{
    public class Mockup
    {
        public List<Author> authorList = new List<Author> { 
            new Author { _id=0, _firstname="Lena", _lastname="AmenShitVasnygg", _birthyear="1942" },
            new Author { _id=0, _firstname="Lena", _lastname="Abdullah", _birthyear="1942" },
            new Author { _id=0, _firstname="Lena", _lastname="Nilsson", _birthyear="1942" },
            new Author { _id=0, _firstname="Lena", _lastname="Lol", _birthyear="1942" },
            new Author { _id=0, _firstname="Åsna", _lastname="Kuk", _birthyear="1942" },
            new Author { _id=0, _firstname="Fitt", _lastname="Bög", _birthyear="1942" },
            new Author { _id=0, _firstname="Hhahah", _lastname="Hitler", _birthyear="1942" },
            new Author { _id=0, _firstname="Lena", _lastname="Harald", _birthyear="1942" },
            new Author { _id=0, _firstname="Luder", _lastname="Nilsson", _birthyear="1942" },
            new Author { _id=0, _firstname="Nisse", _lastname="Allah", _birthyear="1942" }
        };

        //public List<BookDetails> bookDetailsList = new List<BookDetails> { 
        //    new BookDetails { 
        //        _book = new Book { _isbn = 9789137144238, _title = "Bli vän med din pms", _signId = 1, _publicationYear = 2015, _publicationInfo = "Bonnier, Sverige", _pages = 232 },
        //        _author = new Author { _id = 0, _firstname = "Astrid", _lastname = "von Lindgrenovica", _birthyear = "1934" },
        //        _classification = new Classification { _description = "Bonnier", _signId = 1, _signum = "Skräck" }
        //    },

        //    new BookDetails { 
        //        _book = new Book { _isbn = 9789152633359, _title = "Bibel för barn", _signId = 7, _publicationYear = 2016, _publicationInfo = "Bonnier, Sverige", _pages = 271 },
        //        _author = new Author { _id = 0, _firstname = "Astrid", _lastname = "von Lindgrenovica", _birthyear = "1934" },
        //        _classification = new Classification { _description = "Bonnier", _signId = 1, _signum = "Skräck" }
        //    }
        //};

        //public List<Book> bookList = new List<Book>
        //{
        //    new Book { _isbn = 9789137144238, _title = "Bli vän med din pms", _signId = 1, _publicationYear = 2015, _publicationInfo = "Bonnier, Sverige", _pages = 232 },
        //    new Book { _isbn = 9780091949013, _title = "How to Build a Girl", _signId = 2, _publicationYear = 2015, _publicationInfo = "Bill Cosby DrinkGallóre, USA ", _pages = 343 },
        //    new Book { _isbn = 9780691158051, _title = "The Church of Scientology", _signId = 3, _publicationYear = 2013, _publicationInfo = "Tom Cruise PublishInternational, USA", _pages = 280 },
        //    new Book { _isbn = 9781461332701, _title = "Sex Education in the Eighties", _signId = 4, _publicationYear = 2015, _publicationInfo = "Löfven HandelsBolag, Sverige", _pages = 69 },
        //    new Book { _isbn = 9789113043210, _title = "Tobleroneaffären : Varför Sverige inte fick sin första kvinnliga statsminister.", _signId = 5, _publicationYear = 2012, _publicationInfo = "Bonnier, Sverige", _pages = 268 },
        //    new Book { _isbn = 9781494876791, _title = "The Art of Being a Motherfucker", _signId = 6, _publicationYear = 2014, _publicationInfo = "Florén AB, Sverige", _pages = 176 },
        //    new Book { _isbn = 9789152633359, _title = "Bibel för barn", _signId = 7, _publicationYear = 2016, _publicationInfo = "Bonnier, Sverige", _pages = 271 }          
        //};

        public List<Classification> bookDescription = new List<Classification>
        {
            new Classification { _signId = 1, _signum = "Genre", _description = "Varför är det så svårt att prata om pms? Medan man med lätthet talar om laktosintolerans, migrän och nageltrång kan det kännas pinsamt att berätta om sina premenstruella besvär och även att söka hjälp. Det vill Lisa Eriksson råda bot på."},
            new Classification { _signId = 2, _signum = "Genre", _description = "My name's Johanna Morrigan. I'm fourteen, and I've just decided to kill myself. I don't really want to die, of course! I just need to kill Johanna, and build a new girl. Dolly Wilde will be everything I want to be, and more! But as with all the best coming-of-age stories, it doesn't exactly go to plan... This is the brilliant Number One bestselling novel from Caitlin Moran, the award-winning and Sunday Times bestselling author of How to Be a Woman."},
            new Classification { _signId = 3, _signum = "Genre", _description = "Scientology is one of the wealthiest and most powerful new religions to emerge in the past century. To its detractors, L. Ron Hubbard's space-age mysticism is a moneymaking scam and sinister brainwashing cult. But to its adherents, it is humanity's brightest hope. Few religious movements have been subject to public scrutiny like Scientology, yet much of what is written about the church is sensationalist and inaccurate. Here for the first time is the story of Scientology's protracted and turbulent journey to recognition as a religion in the postwar American landscape"},
            new Classification { _signId = 4, _signum = "Genre", _description = "The odd reader may be interested in how a book comes about. Members of the SIECUS Board of Directors were planning a Festschrift and dinner for Mary Calderone on the occasion of her 75th birthday. One planning idea was to have a booklet, filled with brief essays from prominent sex educators, distributed between the roast beef and the ice cream. My reaction was that such find their burial place in the same dusty drawer as the program from the high school prom and ticket stubs from South Pacific."},
            new Classification { _signId = 5, _signum = "Genre", _description = "Mona fimpar cigarretten. På med läppstift. Samma ritual som vanligt. Reser sig ur favoritfåtöljen. Samlar ihop sina handskrivna nedklottrade papper. Det är bara minuter kvar till presskonferensen. Till timeout. Sju våningar ner i Rosenbads pressrum väntar Sveriges hela journalistkår."},
            new Classification { _signId = 6, _signum = "Genre", _description = "The story of an amoral asshole who slept with all of your women, illegally advertised himself all over the city of Cincinnati, danced with 30,000 crazy motherfuckers, questioned the roles of race, sex, disability & freedom of speech in society and made some folks smile along the way... ALL WHILE TRYING TO CURE CANCER. FUCK. YOU"},
            new Classification { _signId = 7, _signum = "Genre", _description = "Bibel för barn är den första helsvenska barnbibeln och den har sålt i flera hundra tusen exemplar seda den första kom ut 1995. Den är trogen den ursprungliga bibeltexten och författarna har lång erfarenhet av arbete med barn och bibeln"}
        };

        public List<LoginData> userList = new List<LoginData>
        {
            new LoginData{_username = "010101", _password = "123", _level="2", _hash="1436862325", _salt="apansson", _personId="1"},
            new LoginData{_username = "020202", _password = "123", _level="1", _hash="454455760", _salt="svanslos", _personId="2"},
            new LoginData{_username = "030303", _password = "123", _level="1", _hash="-1623739142", _salt="honolulu", _personId="3"},
            new LoginData{_username = "040404", _password = "123", _level="2", _hash="-1623739142", _salt="trivialt", _personId="4"},
        };

        public List<Borrower> borrowerList = new List<Borrower>
        {
            new Borrower { _pid = "010101", _catId = 1, _firstname = "Evert", _lastname = "Taube", _address = "Sockeln 12", _phoneno = "0728295003" },
            new Borrower { _pid = "020202", _catId = 2, _firstname = "Adam", _lastname = "Tollin", _address = "Studentbyggnad bredvid Joc 21", _phoneno = "0762154825" },
            new Borrower { _pid = "030303", _catId = 3, _firstname = "Evelina", _lastname = "Von Rósen", _address = "Östermalmsgatan 3", _phoneno = "0739415230" },
            new Borrower { _pid = "040404", _catId = 2, _firstname = "Ahmed", _lastname = "Muhammed", _address = "Kenya 2B", _phoneno = "0706415978" }
        };

        public List<Copy> copyList = new List<Copy>
        {
           new Copy { _isbn = 9789137144238, _status = true, _barcode = "1001", _library = ".Lib", _location = "10" },
           new Copy { _isbn = 9789137144238, _status = true, _barcode = "1002", _library = ".Lib", _location = "11" },
           new Copy { _isbn = 9789137144238, _status = true, _barcode = "1003", _library = ".Lib", _location = "12" },
           new Copy { _isbn = 9789137144238, _status = true, _barcode = "1004", _library = ".Lib", _location = "13" }
        };

        public List<Borrow> borrowList = new List<Borrow>
        {
            new Borrow { _pid = 7504166548, _barcode = "1001", _borrowDate = "Maj", _returnDate = "Juni", _toBeReturnedDate = null },
            new Borrow { _pid = 9001016969, _barcode = "1002", _borrowDate = "Januari", _returnDate = "Mars", _toBeReturnedDate = null },
            new Borrow { _pid = 9912310125, _barcode = "1003", _borrowDate = "April", _returnDate = "Augusti", _toBeReturnedDate = null },
            new Borrow { _pid = 6910252222, _barcode = "1004", _borrowDate = "Februari", _returnDate = "December", _toBeReturnedDate = null }
        };
    }
}