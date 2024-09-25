import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthenticationPageComponent } from './components/authentication-page/authentication-page.component';
import { AuthenticationService } from './shared/services/authentication.service';
import { NavigationBarComponent } from './components/navigation-bar/navigation-bar.component';
import { NgIf } from '@angular/common';
import { MainPageComponent } from './components/main-page/main-page.component';
import { ModalComponent } from './components/modal/modal.component';
import { AlertPopupComponent } from './components/alert-popup/alert-popup.component';
import { ToggleThemeService } from './shared/services/toggle-theme.service';
import { AppTheme } from './shared/models/theme.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AuthenticationPageComponent,
    NavigationBarComponent,
    NgIf,
    MainPageComponent,
    ModalComponent,
    AlertPopupComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  public isDarkTheme: boolean = false;

  constructor(
    private authService: AuthenticationService,
    private readonly toggleThemeService: ToggleThemeService
  ) {}

  ngOnInit() {
    this.authService.checkToken();

    const theme = this.toggleThemeService.getStoredTheme();
    if (theme === AppTheme.LIGHT) this.toggleThemeService.setLightTheme();
    if (theme === AppTheme.DARK) this.toggleThemeService.setDarkTheme();
  }

  isUserLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }
}
