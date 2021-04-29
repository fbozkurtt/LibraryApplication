import { Component, OnInit } from '@angular/core';
import { Client, CreateBookCommand } from '../web-api-client'

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.sass']
})
export class CreateBookComponent implements OnInit {
  createBookCommand: any = {
    title: '',
    author: '',
    isbn: '',
    shortDescription: ''
  }
  constructor(private apiClient: Client) { }

  ngOnInit(): void {
  }

  createBookMeta() {
    // var result = this.auth.getToken(this.getTokenQuery);
    // console.log(result);
    this.apiClient.createBookMeta(CreateBookCommand.fromJS(this.createBookCommand)).subscribe(
      (res) => {
        console.log(res);
        alert('Success')
      },
      (error) => {
        console.error(error);
        var error = JSON.parse(error.response);
        alert("Error");
      });
    return false;
  }

}
