import { Component, OnInit } from '@angular/core';
import { Client, BookDtoPaginatedList } from '../web-api-client';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.sass']
})
export class BooksComponent implements OnInit {
  bookPaginatedList: BookDtoPaginatedList;
  pageNumber = 0;
  pageSize = 3;
  constructor(private apiClient: Client) { }

  ngOnInit(): void {
    this.fetchData(this.pageNumber);
  }

  fetchData(pageNumber: number) {
    this.apiClient.getBooks(pageNumber + 1, this.pageSize).subscribe(
      (res) => {
        console.log(res);
        this.bookPaginatedList = res;
      },
      (error) => {
        console.log(error);
        alert(error);
      }
    );
  }

  reserveBook(isbn: string) {
    this.apiClient.reserveBook(isbn).subscribe(
      (res) => {
        console.log(res);
        alert("Success");
        this.fetchData(this.bookPaginatedList.pageIndex - 1);
      },
      (error) => {
        console.log(error);
        var error = JSON.parse(error.response);
        alert(error.detail);
      }
    );
  }

  addBook(isbn: string) {
    var qrCode = this.generateQrCode(10);
      this.apiClient.addBookToInventory(isbn, qrCode).subscribe(
        (res) => {
          console.log(res);
          alert("Success");
          this.fetchData(this.bookPaginatedList.pageIndex - 1);
        },
        (error) => {
          console.log(error);
          var error = JSON.parse(error.response);
          alert(error.detail);
        }
      );
  }

  generateQrCode(length) {
    var result = [];
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result.push(characters.charAt(Math.floor(Math.random() *
        charactersLength)));
    }
    return result.join('');
  }
  counter(i: number) {
    return new Array(i);
  }
}