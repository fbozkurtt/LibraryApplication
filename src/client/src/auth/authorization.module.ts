import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Client, GetTokenQuery, GetTokenResponse } from '../app/web-api-client'
import { } from 'rxjs'

@NgModule({
  imports: [
    CommonModule
  ],
})
export class ApiAuthorizationModule {
  private GetTokenResponse!: GetTokenResponse;

  constructor(private apiClient: Client) { }

  checkLoginState(): boolean {
    var token = localStorage.getItem('token');
    var expires = new Date(localStorage.getItem('expires'));

    if (token && expires.getMilliseconds() < Date.now())
      return true;

    this.clearLoginState();
    return false;
  }

  getToken(query: GetTokenQuery): any {
    // this.apiClient.getToken(GetTokenQuery.fromJS(query)).subscribe(
    //   (res) => {
    //     this.GetTokenResponse = res;
    //     localStorage.setItem('token', this.GetTokenResponse.token);
    //     localStorage.setItem('username', this.GetTokenResponse.username);
    //     localStorage.setItem('expires', this.GetTokenResponse.expires.toString());
    //   },
    //   (error) => {
    //     console.error(error);
    //     return false;
    //   },
    //   () => {
    //     return true;
    //   }
    // );
    this.apiClient.getToken(GetTokenQuery.fromJS(query))
      .toPromise()
      .then(res => {
        this.GetTokenResponse = res;
        localStorage.setItem('token', this.GetTokenResponse.token);
        localStorage.setItem('username', this.GetTokenResponse.username);
        localStorage.setItem('expires', this.GetTokenResponse.expires.toString());
        return true;
      }).catch(error => {
        console.error(error);
      })
      return false;
  }

  clearLoginState() {
    localStorage.clear();
  }
}
