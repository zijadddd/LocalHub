import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostOut } from '../models/post.model';
import { PostApi } from '../api-constants/post-api.constant';
import { AuthenticationService } from './authentication.service';
import { LikeAndCommentCountOut } from '../models/like-comment.model';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(
    private readonly httpClient: HttpClient,
    private readonly authService: AuthenticationService
  ) {}

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return headers;
  }

  getPosts(): Observable<PostOut[]> {
    return this.httpClient.get<PostOut[]>(PostApi.GET_POSTS, {
      headers: this.getHeaders(),
    });
  }

  getUserLikeAndCommentCount(
    userId: string
  ): Observable<LikeAndCommentCountOut> {
    return this.httpClient.get<LikeAndCommentCountOut>(
      PostApi.GET_USER_LIKES_AND_COMMENTS_COUNT.replace('#', userId),
      { headers: this.getHeaders() }
    );
  }
}
