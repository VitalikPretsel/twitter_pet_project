import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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
    return this.http.post<void>(`${environment.apiUrl}/auth/login`, loginValues);
  }

  signup(signupValues: object) {
    return this.http.post<void>(`${environment.apiUrl}/auth/signup`, signupValues);
  }

  logout() {
    return this.http.delete<void>(`${environment.apiUrl}/auth`);
  }
}
