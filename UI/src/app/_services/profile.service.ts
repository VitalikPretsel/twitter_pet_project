import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient) { }

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
