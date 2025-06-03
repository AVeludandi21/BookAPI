// Book.cs
// This file defines the Book model used by the API. It represents the data structure for a book and includes validation attributes.

using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    // Represents a book entity with properties for ID, title, publisher, and subject.
    public class Book
    {
        // Unique ID for each book (primary key)
        public int Id { get; set; }

        // Book title (required, max length 100)
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string? Title { get; set; }

        // Publisher name (required, max length 50)
        [Required(ErrorMessage = "Publisher is required")]
        [StringLength(50, ErrorMessage = "Publisher cannot exceed 50 characters")]
        public string? Publisher { get; set; }

        // Subject or category (required, max length 50)
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(50, ErrorMessage = "Subject cannot exceed 50 characters")]
        public string? Subject { get; set; }
    }
}
