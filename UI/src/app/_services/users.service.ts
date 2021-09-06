import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  public getUsers() {
    return this.http.get(`${environment.apiUrl}/users`);
  }

  public getCurrentUser() {
    return this.http.get<User>(`${environment.apiUrl}/account`);
  }

  public getUserProfiles(userId) {
    return this.http.get(`${environment.apiUrl}/profiles/getUserProfiles/${userId}`);
  }

  public getCurrentUserName() {
    return this.http.get(`${environment.apiUrl}/account/userName`, { responseType: 'text' });
  }
}
