import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service'

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.sass']
})
export class SignupComponent implements OnInit {
  invalidUser: boolean;
  errors: string[];

  public signupFormStrings = strings.signupForm;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
  }

  ngOnInit(): void {
  }

  signup = (form: NgForm) => {
    this.authenticationService.signup(form.value)
      .pipe(first()).subscribe(response => {
        this.invalidUser = false;
        this.router.navigate(['/home']);
      }, err => {
        this.invalidUser = true;
        this.errors = [];
        let errorObject;

        if (err.error.errors != null) {
          errorObject = err.error.errors;
        }
        else {
          errorObject = err.error;
        }

        for (var errorField in errorObject) {
          errorObject[errorField].forEach(element => {
            this.errors.push(errorField + ': ' + element);
          });
        }
      });
  }
}
