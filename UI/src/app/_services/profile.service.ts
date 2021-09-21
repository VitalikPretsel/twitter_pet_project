import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs'

import { Profile } from '../_models/profile';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private profileChanged = new BehaviorSubject<Profile>(null);
  public profileChangedObservable = this.profileChanged.asObservable();

  constructor(private http: HttpClient) { }

  public changeProfile(profile) {
    this.profileChanged.next(profile);
  }

  public getProfile(profileName) {
    return this.http.get<Profile>(`${environment.apiUrl}/profiles/getbyname/${profileName}`);
  }

  public getFollowings(profileId) {
    return this.http.get<number[]>(`${environment.apiUrl}/followings/profileFollowings/${profileId}`);
  }
}
