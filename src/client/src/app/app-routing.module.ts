import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import {ReservationsComponent} from './reservations/reservations.component';
import {BooksComponent} from './books/books.component';
import {CreateBookComponent} from './create-book/create-book.component';

export const routes: Routes = [

  { path: 'login', component: LoginComponent },
  { path: 'books', component: BooksComponent },
  { path: 'reservations', component: ReservationsComponent },
  { path: 'create', component: CreateBookComponent },
  { path: '',   redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
