import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthenticationPageComponent } from './components/authentication-page/authentication-page.component';
import { BrowserModule } from '@angular/platform-browser';
import { AuthenticationService } from './shared/services/authentication.service';
import { NavigationBarComponent } from './components/navigation-bar/navigation-bar.component';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AuthenticationPageComponent,
    NavigationBarComponent,
    NgIf,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthenticationService) {}

  ngOnInit() {
    this.authService.checkToken();
  }

  isUserLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
}
