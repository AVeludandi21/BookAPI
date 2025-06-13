import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Book {
  // your book properties and methods here
}

import { TestBed } from '@angular/core/testing';

describe('Book', () => {
  let service: Book;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Book);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
