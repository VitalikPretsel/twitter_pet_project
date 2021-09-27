import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  isAuthenticated() {
    return this.http.get<boolean>(`${environment.apiUrl}/auth`);
  }

  login(loginValues: object) {
    return this.http.post<void>(`${environment.apiUrl}/auth/login`, loginValues)
      .pipe(map(() => {
        this.startRefreshTokenTimer();
      }));
  }

  signup(signupValues: object) {
    return this.http.post<void>(`${environment.apiUrl}/auth/signup`, signupValues)
      .pipe(map(() => {
        this.startRefreshTokenTimer();
      }));
  }

  logout() {
    this.stopRefreshTokenTimer();
    return this.http.delete<void>(`${environment.apiUrl}/auth`);
  }

  refreshToken() {
    return this.http.post<void>(`${environment.apiUrl}/auth/refresh`, {})
      .pipe(map(() => {
        this.startRefreshTokenTimer();
      }));
  }

  private refreshTokenTimeout;

  private startRefreshTokenTimer() {
    const timeout = 4 * 60 * 1000;
    this.refreshTokenTimeout = setTimeout(() => this.refreshToken()
    .subscribe(() => {}, () => {
      this.stopRefreshTokenTimer();
    }), timeout);
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout);
  }
}
