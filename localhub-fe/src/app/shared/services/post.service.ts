import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostOut } from '../models/post.model';
import { PostApi } from '../api-constants/post-api.constant';
import { AuthenticationService } from './authentication.service';
import { LikeAndCommentCountOut } from '../models/like-comment.model';
import { UserLikedPostOut } from '../models/user-like.model';
import { LikeOut } from '../models/like.model';

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

  getPost(id: string): Observable<PostOut> {
    return this.httpClient.get<PostOut>(PostApi.GET_POST.replace('#', id), {
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

  didUserLikePost(
    userId: string,
    postId: string
  ): Observable<UserLikedPostOut> {
    return this.httpClient.get<UserLikedPostOut>(
      PostApi.DID_USER_LIKE_POST.replace('#/!', userId + '/' + postId),
      { headers: this.getHeaders() }
    );
  }

  likePost(userId: string, postId: string): Observable<LikeOut> {
    return this.httpClient.post<LikeOut>(
      PostApi.LIKE_POST.replace('#/!', userId + '/' + postId),
      {},
      { headers: this.getHeaders() }
    );
  }
}
