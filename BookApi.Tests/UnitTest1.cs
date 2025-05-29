using Xunit;
using BookApi.Controllers;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookApi.Tests

  /// Test class for BookController - tests all CRUD operations (Create, Read, Update, Delete)
    public class BookControllerTests
    {
    // Controller instance that will be used for all tests
    private readonly BookController _controller;

        public BookControllerTests()
        {
            _controller = new BookController();
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Act
            var result = _controller.GetAllBooks();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(actionResult.Value);
            Assert.Equal(2, books.Count()); // Initial seed data has 2 books
        }

        [Fact]
        public void GetBook_WithValidId_ShouldReturnBook()
        {
            // Arrange
            int validId = 1;

            // Act
            var result = _controller.GetBook(validId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var book = Assert.IsType<Book>(actionResult.Value);
            Assert.Equal("C# Basics", book.Title);
        }

        [Fact]
        public void GetBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var result = _controller.GetBook(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateBook_ShouldAddNewBook()
        {
            // Arrange
            var newBook = new Book 
            { 
                Title = "Test Book", 
                Publisher = "Test Publisher", 
                Subject = "Test Subject" 
            };

            // Act
            var result = _controller.CreateBook(newBook);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Book>>(result);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var book = Assert.IsType<Book>(createdAtResult.Value);
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("Test Publisher", book.Publisher);
            Assert.Equal("Test Subject", book.Subject);
        }

        [Fact]
        public void UpdateBook_WithValidId_ShouldUpdateBook()
        {
            // Arrange
            int validId = 1;
            var updatedBook = new Book 
            { 
                Title = "Updated Book", 
                Publisher = "Updated Publisher", 
                Subject = "Updated Subject" 
            };

            // Act
            var result = _controller.UpdateBook(validId, updatedBook);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var getResult = _controller.GetBook(validId);
            var book = Assert.IsType<Book>(getResult.Value);
            Assert.Equal("Updated Book", book.Title);
            Assert.Equal("Updated Publisher", book.Publisher);
            Assert.Equal("Updated Subject", book.Subject);
        }

        [Fact]
        public void UpdateBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int invalidId = 999;
            var updatedBook = new Book 
            { 
                Title = "Updated Book", 
                Publisher = "Updated Publisher", 
                Subject = "Updated Subject" 
            };

            // Act
            var result = _controller.UpdateBook(invalidId, updatedBook);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteBook_WithValidId_ShouldRemoveBook()
        {
            // Arrange
            int validId = 1;

            // Act
            var result = _controller.DeleteBook(validId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var getResult = _controller.GetBook(validId);
            Assert.IsType<NotFoundResult>(getResult.Result);
        }

        [Fact]
        public void DeleteBook_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var result = _controller.DeleteBook(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}