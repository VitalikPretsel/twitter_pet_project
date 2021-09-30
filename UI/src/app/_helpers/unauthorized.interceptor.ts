import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, EMPTY, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

import { AuthenticationService } from '../_services/authentication.service';
import { UsersService } from '../_services/users.service';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authenticationService: AuthenticationService, private usersService: UsersService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(tap(() => { },
      (err) => {
        console.log("refresh?");
        if (err.status == 401) {
          this.authenticationService.refreshToken()
            .subscribe(() => { console.log("success!")}, () => {
              console.log("still 401");
              this.router.navigate(['/']);
            })
        }
      }));
  //   return next.handle(request).pipe(catchError(err => {
  //     console.log("interceptor");
  //     if ([401, 403].includes(err.status)) {
  //         console.log("err");
  //         // auto logout if 401 or 403 response returned from api
  //         this.authenticationService.logout();
  //     }

  //     const error = (err && err.error && err.error.message) || err.statusText;
  //     console.error(err);
  //     return throwError(error);
  // }))
  }
}
