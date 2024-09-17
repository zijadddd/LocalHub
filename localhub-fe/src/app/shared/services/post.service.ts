import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostOut } from '../models/post.model';
import { PostApi } from '../api-constants/post-api.constant';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(
    private readonly httpClient: HttpClient,
    private readonly authService: AuthenticationService
  ) {}

  getPosts(): Observable<PostOut[]> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.httpClient.get<PostOut[]>(PostApi.GET_POSTS, { headers });
  }
}
