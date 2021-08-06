import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentAuthSubject: BehaviorSubject<boolean>;

  public get currentAuthValue(): boolean {
    return this.currentAuthSubject.value;
  }

  constructor(private http: HttpClient, private cookieService: CookieService) {
    this.currentAuthSubject = new BehaviorSubject<boolean>(false);
  }

  isAuthenticated() {
    return this.http.get<boolean>(`${environment.apiUrl}/auth`)
    .pipe(map(auth => {
      this.currentAuthSubject.next(auth);
    }));
  }

  login(form: any) {
    return this.http.post<any>(`${environment.apiUrl}/auth`, form, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      })
    })
  }

  logout() {
    this.http.delete(`${environment.apiUrl}/auth`).subscribe();
  }
}
