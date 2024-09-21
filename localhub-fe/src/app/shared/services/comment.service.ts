import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommentIn, CommentOut } from '../models/comment.model';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';
import { CommentApi } from '../api-constants/comment-api.costant';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
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

  getAllComments(postId: string): Observable<CommentOut[]> {
    return this.httpClient.get<CommentOut[]>(
      CommentApi.GET_ALL_COMMENTS.replace('#', postId),
      { headers: this.getHeaders() }
    );
  }

  commentPost(
    postId: string,
    userId: string,
    comment: CommentIn
  ): Observable<CommentOut> {
    return this.httpClient.post<CommentOut>(
      CommentApi.COMMENT_POST.replace('#/!', postId + '/' + userId),
      comment,
      { headers: this.getHeaders() }
    );
  }

  deleteComment(commentId: string): Observable<CommentOut> {
    return this.httpClient.delete<CommentOut>(
      CommentApi.DELETE_COMMENT.replace('#', commentId),
      { headers: this.getHeaders() }
    );
  }

  editComment(commentId: string, comment: CommentIn): Observable<CommentOut> {
    return this.httpClient.put<CommentOut>(
      CommentApi.UPDATE_COMMENT.replace('#', commentId),
      comment,
      { headers: this.getHeaders() }
    );
  }
}
