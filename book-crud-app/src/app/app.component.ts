import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BookListComponent } from './components/book-list/book-list.component';
import { BookFormModule } from './components/book-form/book-form.module';
import { BookFormComponent } from './components/book-form/book-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, BookListComponent, BookFormModule, BookFormComponent],
  template: '<router-outlet></router-outlet>'
})
export class AppComponent { }