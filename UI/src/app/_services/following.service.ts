import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FollowingService {

  constructor(private http: HttpClient) { }

  public isfollowing(followerId, followingId) {
    return this.http.get<boolean>(`${environment.apiUrl}/followings/details?followerId=${followerId}&followingId=${followingId}`);
  }

  public follow(followingValues) {
    return this.http.post<void>(`${environment.apiUrl}/followings`, followingValues);
  }

  public unfollow(followerId, followingId) {
    return this.http.delete<void>(`${environment.apiUrl}/followings/details?followerId=${followerId}&followingId=${followingId}`);
  }
}
