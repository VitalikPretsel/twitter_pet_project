import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Post } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private http: HttpClient) { }

  public createPost(postValues) {
    console.log(postValues);
    return this.http.post<void>(`${environment.apiUrl}/posts`, postValues);
  }

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
}
