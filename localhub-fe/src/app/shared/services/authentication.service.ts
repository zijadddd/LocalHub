import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthIn, AuthOut } from '../models/auth.model';
import { Observable } from 'rxjs';
import { AuthenticationApi } from '../api-constants/authentication-api.constant';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from '../models/jwt.model';
import { SuspendIn, SuspendOut } from '../models/suspend.model';
import { MessageOut } from '../models/message-out.model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(
    private readonly httpClient: HttpClient,
    private readonly router: Router
  ) {}

  private getHeaders(): HttpHeaders {
    const token = this.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return headers;
  }

  authenticateUser(request: AuthIn): Observable<AuthOut> {
    return this.httpClient.post<AuthOut>(
      AuthenticationApi.AUTHENTICATION,
      request
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  getToken(): string | null {
    const token = localStorage.getItem('token');
    return token;
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

  getDecodedToken(token: string): JwtPayload | null {
    try {
      return jwtDecode<JwtPayload>(token);
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }

  getNameIdentifierFromToken(token: string): string | null {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken
      ? decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ]
      : null;
  }

  getUserRoleFromToken(token: string): string | null {
    const decodedToken = this.getDecodedToken(token);
    return decodedToken
      ? decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ]
      : null;
  }

  isSuspended(userId: string): Observable<SuspendOut> {
    return this.httpClient.get<SuspendOut>(
      AuthenticationApi.IS_SUSPENDED.replace('#', userId),
      { headers: this.getHeaders() }
    );
  }

  suspendUser(
    userId: string,
    suspendReason: SuspendIn
  ): Observable<MessageOut> {
    return this.httpClient.post<MessageOut>(
      AuthenticationApi.SUSPEND_USER.replace('#', userId),
      suspendReason,
      { headers: this.getHeaders() }
    );
  }
}
