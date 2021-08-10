import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  isAuthenticated() {
    return this.http.get<any>(`${environment.apiUrl}/auth`);
  }

  login(form: any) {
    return this.http.post<any>(`${environment.apiUrl}/auth`, form, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    })
  }

  logout() {
    return this.http.delete(`${environment.apiUrl}/auth`);
  }
}
