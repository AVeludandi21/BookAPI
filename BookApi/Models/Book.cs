using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public class Book
    {
        //model: represents the data the API will work with (the book)
        public int Id { get; set; }  // Unique ID for each book
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string? Title { get; set; }     // Book title

        [Required(ErrorMessage = "Publisher is required")]
        [StringLength(50, ErrorMessage = "Publisher cannot exceed 50 characters")]
        public string? Publisher { get; set; } // Publisher name

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(50, ErrorMessage = "Subject cannot exceed 50 characters")]
        public string? Subject { get; set; }   // Subject or category
    }
}
