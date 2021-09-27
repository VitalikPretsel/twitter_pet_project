import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { Profile } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private currentUserSubject = new BehaviorSubject<User>(null);
  public currentUserObservable = this.currentUserSubject.asObservable();

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  constructor(private http: HttpClient) { }

  public getCurrentUser() {
    return this.http.get<User>(`${environment.apiUrl}/account`)
    .pipe(map(user => {
      this.currentUserSubject.next(user);
      return user;
    }));
  }

  public getUserProfiles(userId) {
    return this.http.get<Profile[]>(`${environment.apiUrl}/profiles/getUserProfiles/${userId}`);
  }

  public getCurrentUserName() {
    return this.http.get<string>(`${environment.apiUrl}/account/userName`, { responseType: 'text' as 'json'});
  }
}
