import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LikesService {

  constructor(private http: HttpClient) { }

  public getProfileLikedPosts(profileFollowerId, profileFollowingIds, lastId) {
    let params = new HttpParams();
    if (lastId != null)
    {
      params = params.append('id', lastId);
    }
    for (let i = 0; i < profileFollowingIds.length; i++)
    {
      params = params.append('profileFollowingIds', profileFollowingIds[i]);
    }
    return this.http.get<number[]>(`${environment.apiUrl}/posts/getProfileLikedPosts/details?profileFollowerId=${profileFollowerId}&step=20`, { params: params });
  }

  public isLiked(profileId, postId) {
    return this.http.get<boolean>(`${environment.apiUrl}/likes/details?profileId=${profileId}&postId=${postId}`);
  }

  public like(likeValues) {
    return this.http.post<void>(`${environment.apiUrl}/likes`, likeValues);
  }

  public unlike(profileId, postId) {
    return this.http.delete<void>(`${environment.apiUrl}/likes/details?profileId=${profileId}&postId=${postId}`);
  }
}
