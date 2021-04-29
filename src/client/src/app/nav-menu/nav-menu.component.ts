import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.sass']
})
export class NavMenuComponent implements OnInit {

  constructor(private router: Router, @Inject(DOCUMENT) private document: Document) { }

  ngOnInit(): void {
  }
  
  logout(){
    localStorage.clear();
    this.document.defaultView.location.replace('/login');
    // this.router.navigate(['login']);
  }
}
