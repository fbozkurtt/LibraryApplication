import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { Client } from './web-api-client'
import { AccessTokenInterceptor } from 'src/auth/access-token.inceptor';
import { ReservationsComponent } from './reservations/reservations.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BooksComponent } from './books/books.component';
import { ApiAuthorizationModule } from '../auth/authorization.module';
import { CreateBookComponent } from './create-book/create-book.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ReservationsComponent,
    NavMenuComponent,
    BooksComponent,
    CreateBookComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule
  ],
  providers: [
    Client,
    { provide: HTTP_INTERCEPTORS, useClass: AccessTokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
