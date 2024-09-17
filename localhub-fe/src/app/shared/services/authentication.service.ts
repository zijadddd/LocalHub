import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthIn, AuthOut } from '../models/auth-in.model';
import { Observable } from 'rxjs';
import { AuthenticationApi } from '../api-constants/authentication-api.constant';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(
    private readonly httpClient: HttpClient,
    private readonly router: Router
  ) {}

  authenticateUser(request: AuthIn): Observable<AuthOut> {
    return this.httpClient.post<AuthOut>(
      AuthenticationApi.AUTHENTICATION,
      request
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  isTokenExpired(token: string): boolean {
    try {
      const decodedToken: any = jwtDecode(token);
      const currentTime = Math.floor(Date.now() / 1000);
      return decodedToken.exp < currentTime;
    } catch (e) {
      return true;
    }
  }

  checkToken() {
    const token = localStorage.getItem('token');
    if (token && this.isTokenExpired(token)) {
      localStorage.clear();
      this.router.navigate(['/login']);
    }
  }
}
