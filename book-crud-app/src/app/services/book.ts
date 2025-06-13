import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Book } from '../models/book.interface';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private mockBooks: Book[] = [
    { id: 1, title: 'Angular Basics', publisher: 'Tech Books', subject: 'Programming' },
    { id: 2, title: 'TypeScript Guide', publisher: 'Code Press', subject: 'Programming' },
    { id: 3, title: 'Material UI Design', publisher: 'Design Books', subject: 'UI/UX' }
  ];

  getBooks(): Observable<Book[]> {
    return of(this.mockBooks);
  }

  getBook(id: number): Observable<Book | undefined> {
    const book = this.mockBooks.find(b => b.id === id);
    return of(book);
  }

  createBook(book: Omit<Book, 'id'>): Observable<Book> {
    const newBook = { ...book, id: this.mockBooks.length + 1 };
    this.mockBooks.push(newBook);
    return of(newBook);
  }

  updateBook(id: number, book: Partial<Book>): Observable<Book> {
    const index = this.mockBooks.findIndex(b => b.id === id);
    if (index === -1) throw new Error('Book not found');
    
    this.mockBooks[index] = { ...this.mockBooks[index], ...book };
    return of(this.mockBooks[index]);
  }

  deleteBook(id: number): Observable<void> {
    const index = this.mockBooks.findIndex(b => b.id === id);
    if (index !== -1) {
      this.mockBooks.splice(index, 1);
    }
    return of(void 0);
  }
}

export type { Book };
