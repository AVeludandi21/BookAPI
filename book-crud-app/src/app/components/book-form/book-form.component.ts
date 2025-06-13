import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Book } from '../../models/book.interface';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.html',
  styleUrls: ['./book-form.component.css'],
  
})
export class BookFormComponent implements OnInit {
  bookForm!: FormGroup;
  isEdit: boolean;
  isLoading = false;
  error: string | null = null;
  isEditMode = false;
  bookId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BookFormComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private bookService: BookService,
    @Inject(MAT_DIALOG_DATA) public data: { book?: Book }
  ) {
    this.isEdit = !!data.book;
  }

  ngOnInit() {
    this.bookForm = this.fb.group({
      title: [this.data.book?.title || '', Validators.required],
      publisher: [this.data.book?.publisher || '', Validators.required],
      subject: [this.data.book?.subject || '', Validators.required]
    });

    // Check if we're in edit mode
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.bookId = +id;
      this.loadBook(this.bookId);
    }
  }


  loadBook(id: number): void {
    this.isLoading = true;
    this.bookService.getBook(id).subscribe({
      next: (book) => {
        if (book) {
          this.bookForm.patchValue({
            title: book.title,
            publisher: book.publisher,
            subject: book.subject
          });
        } else {
          this.error = 'Book not found.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        this.error = 'Failed to load book details.';
        this.isLoading = false;
        console.error('Error loading book:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      this.isLoading = true;
      this.error = null;

      const operation = this.isEditMode
        ? this.bookService.updateBook(this.bookId!, this.bookForm.value)
        : this.bookService.createBook(this.bookForm.value);

      operation.subscribe({
        next: () => {
          this.router.navigate(['/books']);
        },
        error: (error) => {
          this.error = `Failed to ${this.isEditMode ? 'update' : 'create'} book. Please try again.`;
          this.isLoading = false;
          console.error('Error saving book:', error);
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/books']);
  }
}
