import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(private http: HttpClient) { }

  public getPosts(lastId) {
    return this.http.get(`${environment.apiUrl}/posts/details?step=20&&id=${lastId}`);
  }

  public getLikesAmount(postId) {
    return this.http.get(`${environment.apiUrl}/likes/onPostAmount/${postId}`);
  }

  public getRepliesAmount(postId) {
    return this.http.get(`${environment.apiUrl}/replies/onPostAmount/${postId}`);
  }

  public getProfileName(profileId) {
    return this.http.get(`${environment.apiUrl}/profiles/profileName/${profileId}`, { responseType: 'text' });
  }
}
