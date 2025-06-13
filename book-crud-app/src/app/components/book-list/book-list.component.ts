import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { BookService, Book } from '../../services/book';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BookFormComponent } from '../book-form/book-form.component';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatDialogModule,
    MatProgressSpinnerModule
  ]
})
export class BookListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'title', 'publisher', 'subject', 'actions'];
  dataSource = new MatTableDataSource<Book>([]);
  isLoading = false;
  error: string | null = null;

  constructor(
    private bookService: BookService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.isLoading = true;
    this.bookService.getBooks().subscribe({
      next: (books) => {
        this.dataSource.data = books;
        this.isLoading = false;
      },
      error: (error) => {
        this.error = 'Failed to load books';
        this.isLoading = false;
        console.error('Error loading books:', error);
      }
    });
  }

  openAddDialog() {
    const dialogRef = this.dialog.open(BookFormComponent, {
      data: { book: null }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.bookService.createBook(result).subscribe({
          next: () => {
            this.loadBooks();
            this.snackBar.open('Book added successfully', 'Close', {
              duration: 3000
            });
          },
          error: (error) => {
            this.snackBar.open('Failed to add book', 'Close', {
              duration: 3000
            });
          }
        });
      }
    });
  }

  openEditDialog(book: Book) {
    const dialogRef = this.dialog.open(BookFormComponent, {
      data: { book }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (book.id === undefined) {
          this.snackBar.open('Book ID is missing. Cannot update book.', 'Close', {
            duration: 3000
          });
          return;
        }
        this.bookService.updateBook(book.id, result).subscribe({
          next: () => {
            this.loadBooks();
            this.snackBar.open('Book updated successfully', 'Close', {
              duration: 3000
            });
          },
          error: (error) => {
            this.snackBar.open('Failed to update book', 'Close', {
              duration: 3000
            });
          }
        });
      }
    });
  }

  deleteBook(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.isLoading = true;
      this.bookService.deleteBook(id).subscribe({
        next: () => {
          this.loadBooks();
        },
        error: (error) => {
          this.error = 'Failed to delete book';
          this.isLoading = false;
          console.error('Error deleting book:', error);
        }
      });
    }
  }
}