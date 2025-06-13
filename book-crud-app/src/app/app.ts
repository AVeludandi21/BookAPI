import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BookList } from './components/book-list/book-list';
import { BookFormComponent } from './components/book-form/book-form.component';
import { BookFormModule } from './components/book-form/book-form.module';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    BookList,
    BookFormModule,
    BookFormComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  title = 'book-crud-app';
}
