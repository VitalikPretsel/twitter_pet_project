import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Post } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private http: HttpClient) { }

  public getProfilesPosts(profileIds, lastId) {
    let params = new HttpParams();
    if (lastId != null)
    {
      params = params.append('id', lastId);
    }
    for (let i = 0; i < profileIds.length; i++)
    {
      params = params.append('profileIds', profileIds[i]);
    }
    return this.http.get<Post[]>(`${environment.apiUrl}/posts/getFewProfilePosts/details?step=20`, { params: params });
  }

  public getLikesAmount(postId) {
    return this.http.get<number>(`${environment.apiUrl}/likes/onPostAmount/${postId}`);
  }

  public getRepliesAmount(postId) {
    return this.http.get<number>(`${environment.apiUrl}/replies/onPostAmount/${postId}`);
  }

  public getProfileName(profileId) {
    return this.http.get<string>(`${environment.apiUrl}/profiles/profileName/${profileId}`, { responseType: 'text' as 'json'});
  }
}
