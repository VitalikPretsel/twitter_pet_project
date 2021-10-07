import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LikesService {

  constructor(private http: HttpClient) { }

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
