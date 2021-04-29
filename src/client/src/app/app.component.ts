import { Component, OnInit, OnChanges } from '@angular/core';
import { ApiAuthorizationModule } from '../auth/authorization.module'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit, OnChanges  {
  title = 'client';
  loggedIn = false;

  constructor(private auth: ApiAuthorizationModule){}

  ngOnInit(): void {
    this.loggedIn = this.auth.checkLoginState();
  }

  ngOnChanges(): void {
    this.loggedIn = this.auth.checkLoginState();
  }
}
