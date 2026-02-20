using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
 using SCD_Homework_1.Services;
namespace SCD_Homework_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController()
        {
            _bookService = new BookService();
        }
        [HttpGet("RetrievalOfAllBooks")]
        public ActionResult<IEnumerable<Book>> RetrieveAllTheBooks()
        {
            try
            {
                var books = _bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retreiving the books.");
            }
        }
        [HttpGet("BookById/{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            try
            {
                var book = _bookService.GetBookById(id);
                if (book == null)
                    return NotFound($"Book with this {id} not found.");
                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retreiving the books.");
            }
        }
        [HttpGet("BookByGenre/{genre}")]
        public ActionResult<IEnumerable<Book>> GetBooksByGenre(string genre)
        {
            try
            {
                var books = _bookService.GetBooksByGenre(genre);
                if (books == null)
                    return NotFound("Books with this genre don't exist.");
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retreiving the books.");
            }
        }
        [HttpGet("BooksByPriceRange/{minPrice}/{maxPrice}")]
        public ActionResult<IEnumerable<Book>> GetBooksByPriceRange(decimal minPrice , decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                return BadRequest();
            if(minPrice > maxPrice)
                return BadRequest();
            try
            {
                var books = _bookService.GetBooksByPriceRange(minPrice, maxPrice);
                if (books == null)
                    return NotFound("Books within this price range not found.");
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retreiving the books.");
            }
        }
        [HttpPost("AddBook")]
        public ActionResult CreateBook(Book book)
        {
            try
            {
                var createdBook = _bookService.AddBook(book);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.BookId }, createdBook);
                
            }

            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retreiving the books.");
            }
        }
        [HttpPut("UpdateBook/{id}")]
        public ActionResult UpdateBook(int id , Book updatedBook)
        {
            try
            {
                var updated = _bookService.UpdateBook(id, updatedBook);
                if (!updated)
                {
                    return NotFound($"Book with ID {id} not found");
                }
                return NoContent();
            }
            
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the book");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var deleted = _bookService.DeleteBook(id);
                if (!deleted)
                {
                    return NotFound($"Book with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the book");
            }
        }
    }

}
