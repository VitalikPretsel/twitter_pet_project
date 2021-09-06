import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service'

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.sass']
})
export class WelcomeComponent implements OnInit {

  public welcomeStrings = strings.welcome;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.isAuthenticated().pipe(map(
      (res) => {
        if (res) {
          this.router.navigate(['/home']);
        }
      })).subscribe();
   }

  ngOnInit(): void {
  }

}
