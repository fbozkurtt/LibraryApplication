import { Component, OnInit } from '@angular/core';
import { Client, BookReservationDtoPaginatedList, ReturnBookCommand } from '../web-api-client';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.sass']
})
export class ReservationsComponent implements OnInit {
  bookPaginatedList: BookReservationDtoPaginatedList;
  pageNumber = 0;
  pageSize = 5;
  returnBookCommand: any = {
    qrCode: '',
    fee: 0
  }
  constructor(private apiClient: Client) { }

  ngOnInit(): void {
    this.fetchData(this.pageNumber);
  }

  fetchData(pageNumber: number) {
    this.apiClient.getReservations(pageNumber + 1, this.pageSize).subscribe(
      (res) => {
        console.log(res);
        this.bookPaginatedList = res;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  returnBook(qrCode: string) {
    this.returnBookCommand.qrCode = qrCode;
    this.apiClient.returnBook(ReturnBookCommand.fromJS(this.returnBookCommand)).subscribe(
      (res) => {
        console.log(res);
        alert("Success");
        this.fetchData(this.bookPaginatedList.pageIndex - 1);
      },
      (error) => {
        console.log(error);
        alert("Error");
      }
    );
  }

  counter(i: number) {
    return new Array(i);
  }
}
