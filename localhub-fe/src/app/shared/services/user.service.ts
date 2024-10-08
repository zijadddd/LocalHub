import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserApi } from '../api-constants/user-api.constant';
import { PictureIn, PictureOut } from '../models/picture.model';
import { AuthenticationService } from './authentication.service';
import { UserOut } from '../models/user.model';
import { RoleOut } from '../models/role.model';
import { MessageOut } from '../models/message-out.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
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

  getUserProfilePhoto(id: string): Observable<PictureOut> {
    return this.httpClient.get<PictureOut>(
      UserApi.GET_USER_PROFILE_PHOTO.replace('#', id),
      { headers: this.getHeaders() }
    );
  }

  getUser(id: string): Observable<UserOut> {
    return this.httpClient.get<UserOut>(UserApi.GET_USER.replace('#', id), {
      headers: this.getHeaders(),
    });
  }

  getUserRole(id: string): Observable<RoleOut> {
    console.log(id);
    return this.httpClient.get<RoleOut>(
      UserApi.GET_USER_ROLE.replace('#', id),
      {
        headers: this.getHeaders(),
      }
    );
  }

  changeProfilePhoto(id: string, image: FormData): Observable<PictureOut> {
    return this.httpClient.put<PictureOut>(
      UserApi.CHANGE_PROFILE_PICTURE.replace('#', id),
      image,
      { headers: this.getHeaders() }
    );
  }

  getAllUsers(): Observable<UserOut[]> {
    return this.httpClient.get<UserOut[]>(UserApi.GET_ALL_USERS, {
      headers: this.getHeaders(),
    });
  }

  deleteUser(id: string): Observable<MessageOut> {
    return this.httpClient.delete<MessageOut>(
      UserApi.DELETE_USER.replace('#', id),
      { headers: this.getHeaders() }
    );
  }
}
