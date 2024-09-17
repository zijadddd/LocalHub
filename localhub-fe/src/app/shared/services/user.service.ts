import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserApi } from '../api-constants/user-api.constant';
import { PictureOut } from '../models/picture.model';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private readonly httpClient: HttpClient,
    private readonly authService: AuthenticationService
  ) {}

  getUserProfilePhoto(id: string): Observable<PictureOut> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    return this.httpClient.get<PictureOut>(
      UserApi.GET_USER_PROFILE_PHOTO.replace('#', id),
      { headers }
    );
  }
}
