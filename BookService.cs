using LibraryManagementSystem.Models;
namespace SCD_Homework_1.Services
{
    public class BookService
    {
       private readonly  List<Book> _books = new List<Book>();
        private int initialId = 1;
        public BookService() { }
        public Book AddBook(Book book)
        {
            if(_books.Any(b=> b.ISBN == book.ISBN))
            {
                throw new InvalidOperationException("A book with this ISBN already exists.");
            }
            book.BookId = initialId++;
;           _books.Add(book);
            return book;
        }
        public List<Book> GetAllBooks()
        {
            return _books.ToList();
        }
        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b=> b.BookId == id);
        }
        public bool UpdateBook( int id , Book updatedBook)
        {
            var bookById = GetBookById(id);
            if(bookById == null)
            {
                return false;   
            }
            if(bookById.ISBN != updatedBook.ISBN && _books.Any(b=> b.ISBN == updatedBook.ISBN))
            {
                throw new InvalidOperationException("A book with this ISBN already exists.");
            }
            bookById.ISBN = updatedBook.ISBN;
            bookById.Author = updatedBook.Author;
            bookById.Price = updatedBook.Price;
            bookById.PublicationYear = updatedBook.PublicationYear;
            bookById.Title = updatedBook.Title;
            bookById.Genre = updatedBook.Genre;
            bookById.QuantityAvailable = updatedBook.QuantityAvailable;
            return true;
        }
        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if(book == null)
            {
                return false;
            }
            return _books.Remove(book);
        }
        public List<Book> GetBooksByPriceRange(decimal minPrice, decimal maxPrice)
        {
           return _books.Where(book=>book.Price >= minPrice && book.Price <= maxPrice).ToList();
        }
        public List<Book> GetBooksByGenre(string genre)
        {
            return _books.Where(book => book.Genre  == genre).ToList();
        }
    }
}
