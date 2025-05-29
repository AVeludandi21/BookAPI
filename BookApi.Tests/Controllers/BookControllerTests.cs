using Xunit;
using BookApi.Controllers;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookApi.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly BookController _controller;

        public BookControllerTests()
        {
            // Initialize the controller
            _controller = new BookController();
        }

        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            // Act
            var result = _controller.GetAllBooks();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Book>>>(result);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public void GetBook_ReturnsBook_WhenIdExists()
        {
            // Act
            var result = _controller.GetBook(1);

            // Assert
            Assert.IsType<ActionResult<Book>>(result);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public void GetBook_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Act
            var result = _controller.GetBook(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateBook_AddsBookToList()
        {
            // Arrange
            var newBook = new Book { Title = "New Book", Publisher = "Test Publisher", Subject = "Test Subject" };

            // Act
            var result = _controller.CreateBook(newBook);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(result.Value);
            Assert.Equal("New Book", result.Value.Title);
        }
    }
}