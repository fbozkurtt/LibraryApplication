import { Component, Inject } from '@angular/core';
import { ApiAuthorizationModule } from '../../auth/authorization.module'
import { Router } from '@angular/router';
import { DOCUMENT } from '@angular/common';
import { Client, GetTokenResponse, GetTokenQuery } from '../web-api-client'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent {
  getTokenResponse: GetTokenResponse;
  getTokenQuery: any = {
    username: '',
    password: ''
  }

  constructor(private auth: ApiAuthorizationModule, private router: Router, @Inject(DOCUMENT) private document: Document, private apiClient: Client) {
    if (auth.checkLoginState())
      this.router.navigate(['books']);
  }

  login() {
    // var result = this.auth.getToken(this.getTokenQuery);
    // console.log(result);
    this.apiClient.getToken(GetTokenQuery.fromJS(this.getTokenQuery))
      .toPromise()
      .then(res => {
        this.getTokenResponse = res;
        localStorage.setItem('token', this.getTokenResponse.token);
        localStorage.setItem('username', this.getTokenResponse.username);
        localStorage.setItem('expires', this.getTokenResponse.expires.toString());
        // this.router.navigate(['books']).then();
        this.document.defaultView.location.replace('/books');
      }).catch(error => {
        console.error(error);
        var error = JSON.parse(error.response);
        alert(error.detail);
      })
  }
}