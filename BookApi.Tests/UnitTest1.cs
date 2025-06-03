using Xunit;
using BookApi.Controllers;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// This file contains unit tests for the BookController class, covering all CRUD operations.

namespace BookApi.Tests
{
    /// <summary>
    /// Test class for BookController - tests all CRUD operations (Create, Read, Update, Delete)
    /// </summary>
    public class BookControllerTests
    {
        // Controller instance that will be used for all tests
        private readonly BookController _controller;

        /// <summary>
        /// Initializes a new instance of the BookController for testing.
        /// </summary>
        public BookControllerTests()
        {
            _controller = new BookController();
        }

        /// <summary>
        /// Tests that GetAllBooks returns all books in the initial seed data.
        /// </summary>
        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Act: Call the method to get all books
            var result = _controller.GetAllBooks();

            // Assert: Check that the result is an ActionResult containing a collection of books
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(actionResult.Value);
            Assert.Equal(2, books.Count()); // Initial seed data has 2 books
        }

        /// <summary>
        /// Tests that GetBook returns the correct book when a valid ID is provided.
        /// </summary>
        [Fact]
        public void GetBook_WithValidId_ShouldReturnBook()
        {
            // Arrange: Use a valid book ID from the seed data
            int validId = 1;

            // Act: Call the method to get the book by ID
            var result = _controller.GetBook(validId);

            // Assert: Check that the returned book matches the expected title
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var book = Assert.IsType<Book>(actionResult.Value);
            Assert.Equal("C# Basics", book.Title);
        }

        /// <summary>
        /// Tests that GetBook returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public void GetBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange: Use an invalid book ID
            int invalidId = 999;

            // Act: Attempt to get a book with a non-existent ID
            var result = _controller.GetBook(invalidId);

            // Assert: Should return NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        /// <summary>
        /// Tests that CreateBook adds a new book and returns the created book.
        /// </summary>
        [Fact]
        public void CreateBook_ShouldAddNewBook()
        {
            // Arrange: Create a new book object
            var newBook = new Book 
            { 
                Title = "Test Book", 
                Publisher = "Test Publisher", 
                Subject = "Test Subject" 
            };

            // Act: Call the method to create a new book
            var result = _controller.CreateBook(newBook);

            // Assert: Check that the book was created and returned correctly
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var book = Assert.IsType<Book>(createdAtResult.Value);
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("Test Publisher", book.Publisher);
            Assert.Equal("Test Subject", book.Subject);
        }

        /// <summary>
        /// Tests that UpdateBook updates an existing book when a valid ID is provided.
        /// </summary>
        [Fact]
        public void UpdateBook_WithValidId_ShouldUpdateBook()
        {
            // Arrange: Prepare updated book data for a valid ID
            int validId = 1;
            var updatedBook = new Book 
            { 
                Title = "Updated Book", 
                Publisher = "Updated Publisher", 
                Subject = "Updated Subject" 
            };

            // Act: Call the method to update the book
            var result = _controller.UpdateBook(validId, updatedBook);

            // Assert: Should return NoContentResult and the book should be updated
            Assert.IsType<NoContentResult>(result);
            var getResult = _controller.GetBook(validId);
            var book = Assert.IsType<Book>(getResult.Value);
            Assert.Equal("Updated Book", book.Title);
            Assert.Equal("Updated Publisher", book.Publisher);
            Assert.Equal("Updated Subject", book.Subject);
        }

        /// <summary>
        /// Tests that UpdateBook returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public void UpdateBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange: Prepare updated book data for an invalid ID
            int invalidId = 999;
            var updatedBook = new Book 
            { 
                Title = "Updated Book", 
                Publisher = "Updated Publisher", 
                Subject = "Updated Subject" 
            };

            // Act: Attempt to update a non-existent book
            var result = _controller.UpdateBook(invalidId, updatedBook);

            // Assert: Should return NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Tests that DeleteBook removes a book when a valid ID is provided.
        /// </summary>
        [Fact]
        public void DeleteBook_WithValidId_ShouldRemoveBook()
        {
            // Arrange: Use a valid book ID
            int validId = 1;

            // Act: Call the method to delete the book
            var result = _controller.DeleteBook(validId);

            // Assert: Should return NoContentResult and the book should no longer exist
            Assert.IsType<NoContentResult>(result);
            var getResult = _controller.GetBook(validId);
            Assert.IsType<NotFoundResult>(getResult.Result);
        }

        /// <summary>
        /// Tests that DeleteBook returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public void DeleteBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange: Use an invalid book ID
            int invalidId = 999;

            // Act: Attempt to delete a non-existent book
            var result = _controller.DeleteBook(invalidId);

            // Assert: Should return NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }
    }
}