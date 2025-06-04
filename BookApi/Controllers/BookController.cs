// BookController.cs
// This controller provides a simple in-memory CRUD API for managing books.
// It demonstrates basic usage of ASP.NET Core Web API controllers.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookApi.Models; // Brings in the Book model definition
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Controllers
{
    [ApiController] // Indicates this is a Web API controller
    [Route("api/[controller]")] // Route: api/book
   
    // Controller for managing books. Supports CRUD operations (Create, Read, Update, Delete).
    public class BookController : ControllerBase
    {
        // Static in-memory list to store books for demonstration purposes.
        // This simulates a database for the lifetime of the application.
        private static readonly List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Basics", Publisher = "Tech Press", Subject = "Programming" },
            new Book { Id = 2, Title = "ASP.NET Core Guide", Publisher = "Web Press", Subject = "Web Development" }
        };

        // Gets all books.
        //returns list of all books.
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            // Return the full list of books with HTTP 200 OK.
            return Ok(Books);
        }

        // Gets a single book by its ID.
        // returns The book if found, or 404 Not Found.
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            // Find the book with the specified ID.
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                // Return 404 if not found.
                return NotFound();
            }
            // Return the book with HTTP 200 OK.
            return Ok(book);
        }

        // Creates a new book.
        [HttpPost]
        public ActionResult<Book> CreateBook(Book newBook)
        {
            // Assign a new unique ID to the book.
            newBook.Id = Books.Max(b => b.Id) + 1;
            // Add the new book to the list.
            Books.Add(newBook);
            // Return 201 Created with a route to the new book.
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        // Updates an existing book.
        // 204 No Content if successful, or 404 Not Found
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            // Find the book to update.
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                // Return 404 if not found.
                return NotFound();
            }

            // Update the book's properties.
            book.Title = updatedBook.Title;
            book.Publisher = updatedBook.Publisher;
            book.Subject = updatedBook.Subject;

            // Return 204 No Content to indicate success.
            return NoContent();
        }

        // Deletes a book by its ID.
        // 204 No Content if deleted, or 404 Not Found.
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Find the book to delete.
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                // Return 404 if not found.
                return NotFound();
            }

            // Remove the book from the list.
            Books.Remove(book);
            // Return 204 No Content to indicate success.
            return NoContent();
        }
    }
}
