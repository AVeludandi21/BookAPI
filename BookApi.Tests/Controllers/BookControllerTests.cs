// Unit tests for BookController covering basic CRUD operations.
// These tests verify the controller's behavior for getting, creating, and handling not found cases.

using Xunit;
using BookApi.Controllers;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookApi.Tests.Controllers
{
    // Contains unit tests for the BookController class.
    public class BookControllerTests
    {
        // The controller we are testing.
        private readonly BookController _controller;

        // Initializes a new instance of BookController for each test.

        public BookControllerTests()
        {
            // Initialize the controller
            _controller = new BookController();
        }

        // Tests that GetAllBooks returns all books in the list.
        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            // Act: Call the method to get all books
            var result = _controller.GetAllBooks();

            // Assert: The result should be an ActionResult with a non-empty list of books
            Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            Assert.NotEmpty(result.Value);
        }


        // Tests that GetBook returns a book when a valid ID exists.
        [Fact]
        public void GetBook_ReturnsBook_WhenIdExists()
        {
            // Act: Attempt to get a book with a valid ID (assumed to exist in seed data)
            var result = _controller.GetBook(1);

            // Assert: The result should be an ActionResult with a non-null book and correct ID
            Assert.IsType<ActionResult<Book>>(result);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        // Tests that GetBook returns NotFound when the ID does not exist.
        [Fact]
        public void GetBook_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Act: Attempt to get a book with an invalid ID
            var result = _controller.GetBook(999);

            // Assert: The result should be a NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Tests that CreateBook adds a new book to the list.
        [Fact]
        public void CreateBook_AddsBookToList()
        {
            // Arrange: Create a new book object
            var newBook = new Book { Title = "New Book", Publisher = "Test Publisher", Subject = "Test Subject" };

            // Act: Call the method to create a new book
            var result = _controller.CreateBook(newBook);

            // Assert: The result should be a CreatedAtActionResult with the correct book data
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(result.Value);
            Assert.Equal("New Book", result.Value.Title);
        }
    }
}