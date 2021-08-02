import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  private isTokenExpired(token: string) {
    const expiry = (JSON.parse(atob(token?.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }

  isAuthenticated() {
    const token: string = this.cookieService.get("X-Access-Token");
    return token && !this.isTokenExpired(token);
  }

  login(form: any) {
    return this.http.post<any>(`${environment.apiUrl}/auth`, form, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      }),
      withCredentials: true,
    })
  }

  logout() {
    this.cookieService.delete("X-Access-Token");
  }
}
