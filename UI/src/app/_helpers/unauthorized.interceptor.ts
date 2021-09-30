import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpContextToken
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

import { AuthenticationService } from '../_services/authentication.service';
import { UsersService } from '../_services/users.service';

export const BYPASS_LOG = new HttpContextToken(() => false);

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {

  constructor(private router: Router, private authenticationService: AuthenticationService, private usersService: UsersService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.context.get(BYPASS_LOG) == true) {
      return next.handle(request);
    }
    return next.handle(request).pipe(tap(() => { },
      async (err) => {
        if (err.status == 401) {
          this.authenticationService.refreshToken()
            .subscribe(() => {
              location.reload();
            }, () => {
              this.router.navigate(['/']);
            })
        }
      }));
  }
}
