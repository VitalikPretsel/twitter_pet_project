import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { Profile } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  public getCurrentUser() {
    return this.http.get<User>(`${environment.apiUrl}/account`);
  }

  public getUserProfiles(userId) {
    return this.http.get<Profile[]>(`${environment.apiUrl}/profiles/getUserProfiles/${userId}`);
  }

  public getCurrentUserName() {
    return this.http.get<string>(`${environment.apiUrl}/account/userName`, { responseType: 'text' as 'json'});
  }
}
