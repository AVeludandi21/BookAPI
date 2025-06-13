import { Routes } from '@angular/router';
import { BookList } from './components/book-list/book-list';
import { BookFormComponent } from './components/book-form/book-form.component';

export const routes: Routes = [
    { path: '', redirectTo: '/books', pathMatch: 'full' },
    { path: 'books', component: BookList },
    { path: 'books/new', component: BookFormComponent },
    { path: 'books/:id/edit', component: BookFormComponent }
];
