import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { first, map } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service'

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.sass']
})
export class SignupComponent implements OnInit {
  invalidUser: boolean;

  public signupFormStrings = strings.signupForm;

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
  
  signup = (form: NgForm) => {
    this.authenticationService.signup(JSON.stringify(form.value))
      .pipe(first()).subscribe(response => {
        this.invalidUser = false;
        this.router.navigate(['/home']);
      }, err => {
        this.invalidUser = true;
      });
  }
}
