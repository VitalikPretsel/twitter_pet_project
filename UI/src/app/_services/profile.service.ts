import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs'

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  public profileChanged = new BehaviorSubject<any>(null);
  profileChangedObservable = this.profileChanged.asObservable();

  constructor(private http: HttpClient) { }

  public changeProfile(profile) {
    this.profileChanged.next(profile);
  }

  public getProfile(profileName) {
    return this.http.get(`${environment.apiUrl}/profiles/getbyname/${profileName}`);
  }

  public getFollowersAmount(profileId) {
    return this.http.get(`${environment.apiUrl}/followings/profileFollowersAmount/${profileId}`);
  }

  public getFollowingsAmount(profileId) {
    return this.http.get(`${environment.apiUrl}/followings/profileFollowingsAmount/${profileId}`);
  }

  public getFollowings(profileId) {
    return this.http.get<number[]>(`${environment.apiUrl}/followings/profileFollowings/${profileId}`);
  }

  public getPostsAmount(profileId) {
    return this.http.get(`${environment.apiUrl}/posts/profilePostsAmount/${profileId}`);
  }
}
