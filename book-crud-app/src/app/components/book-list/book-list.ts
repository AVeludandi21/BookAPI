import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { BookService } from '../../services/book';
import { Book } from '../../models/book.interface';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './book-list.html',
  styleUrl: './book-list.scss'
})
export class BookList implements OnInit {
  books: Book[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(
    private bookService: BookService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.isLoading = true;
    this.error = null;
    
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.isLoading = false;
      },
      error: (error) => {
        this.error = 'Failed to load books. Please try again.';
        this.isLoading = false;
        console.error('Error loading books:', error);
      }
    });
  }

  onDelete(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.bookService.deleteBook(id).subscribe({
        next: () => {
          this.books = this.books.filter(book => book.id !== id);
        },
        error: (error) => {
          this.error = 'Failed to delete book. Please try again.';
          console.error('Error deleting book:', error);
        }
      });
    }
  }

  onEdit(id: number): void {
    this.router.navigate(['/books', id, 'edit']);
  }

  onAdd(): void {
    this.router.navigate(['/books/new']);
  }
}
