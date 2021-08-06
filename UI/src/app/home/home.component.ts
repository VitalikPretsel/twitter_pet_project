import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../_services/authentication.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  private isAuthenticated: boolean;
  public get isAuthenticatedValue(): boolean {
    return this.isAuthenticated;
  }

  constructor(private router: Router, private authenticationService: AuthenticationService) { }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
  }

  logOut() {
    this.authenticationService.logout().subscribe(res => {
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
    });
  }
}
