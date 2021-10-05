import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs'

import { Profile } from '../_models/profile';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  private currentProfileSubject = new BehaviorSubject<Profile>(null);
  public currentProfileObservable = this.currentProfileSubject.asObservable();

  public get currentProfileValue(): Profile {
    return this.currentProfileSubject.value;
  }

  constructor(private http: HttpClient) { }

  public changeProfile(profile) {
    this.currentProfileSubject.next(profile);
  }

  public getProfile(profileName) {
    return this.http.get<Profile>(`${environment.apiUrl}/profiles/getbyname/${profileName}`);
  }

  public createProfile(profileValues) {
    return this.http.post<void>(`${environment.apiUrl}/profiles`, profileValues);
  }

  public getFollowings(profileId) {
    return this.http.get<number[]>(`${environment.apiUrl}/followings/profileFollowings/${profileId}`);
  }
}
