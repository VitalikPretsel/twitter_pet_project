import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service'

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  returnUrl: string;

  public loginFormStrings = strings.loginForm;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService
  ) {
  }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
  }

  public login = (form: NgForm) => {
    this.authenticationService.login(form.value)
      .pipe(first()).subscribe(response => {
        this.invalidLogin = false;
        this.router.navigate([this.returnUrl]);
      }, err => {
        this.invalidLogin = true;
      });
  }
}
