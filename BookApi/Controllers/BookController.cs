using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookApi.Models; //brings Book model
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Controllers
{
    [ApiController] //tells ASP.NET this is a Web API controller
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // In-memory book list
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Basics", Publisher = "O'Reilly", Subject = "Programming" },
            new Book { Id = 2, Title = "ASP.NET Core", Publisher = "Microsoft Press", Subject = "Web Dev" }
        };

        // GET: api/book
        // returns all books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return books;
        }

        // GET: api/book/1
        // returns a book by ID if it exists
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); //returns 404 if not found
            return book; //returns 200 with the book
        }

        // POST: api/book
        // adds a new book to the list
        [HttpPost]
        public ActionResult<Book> CreateBook(Book newBook)
        {
            // assigns a new ID
            newBook.Id = books.Max(b => b.Id) + 1;

            // add to list
            books.Add(newBook);

            //returns 201 created and the location of the new book
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        // PUT: api/book/1
        // updates a book by ID
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // 404 if book does not exist

            // updating the book's information
            book.Title = updatedBook.Title;
            book.Publisher = updatedBook.Publisher;
            book.Subject = updatedBook.Subject;

            return NoContent(); // 204 No Content
        }

        // DELETE: api/book/1
        // deletes a book by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // 404 if book does not exist

            books.Remove(book);
            return NoContent(); // 204 success
        }
    }
}
