import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../_services/authentication.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService) { 
    this.authenticationService.isAuthenticated().subscribe();
  }

  ngOnInit(): void {
  }
  
  isUserAuthenticated() {
    return this.authenticationService.currentAuthValue;
  }

  logOut() {
    this.authenticationService.logout();
  }
}
