import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { UsersService } from './users.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private userService: UsersService) { }

  isAuthenticated() {
    return this.http.get<boolean>(`${environment.apiUrl}/auth`);
  }

  login(loginValues: object) {
    return this.http.post<void>(`${environment.apiUrl}/auth/login`, loginValues)
      .pipe(map(() => {
        this.userService.getCurrentUser().subscribe();
      }));
  }

  signup(signupValues: object) {
    return this.http.post<void>(`${environment.apiUrl}/auth/signup`, signupValues)
      .pipe(map(() => {
        this.userService.getCurrentUser().subscribe();
      }));
  }

  logout() {
    return this.http.delete<void>(`${environment.apiUrl}/auth`);
  }

  refreshToken() {
    return this.http.post<void>(`${environment.apiUrl}/auth/refresh`, {});
  }
}
