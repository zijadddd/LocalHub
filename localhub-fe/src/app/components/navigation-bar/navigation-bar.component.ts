import { NgClass, NgFor, NgIf } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { PictureOut } from '../../shared/models/picture.model';
import { UserService } from '../../shared/services/user.service';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { Router, RouterModule } from '@angular/router';
import { CommunicationService } from '../../shared/services/communication.service';
import { WhichAction } from '../../shared/models/which-action.model';
import { ToggleThemeService } from '../../shared/services/toggle-theme.service';
import { AppTheme } from '../../shared/models/theme.model';

@Component({
  selector: 'app-navigation-bar',
  standalone: true,
  imports: [NgClass, NgIf, NgFor, RouterModule],
  templateUrl: './navigation-bar.component.html',
  styleUrl: './navigation-bar.component.css',
})
export class NavigationBarComponent implements OnInit {
  public userProfilePhoto: PictureOut = new PictureOut();
  public isDarkMode = false;
  private authenticatedUserId: string;

  profileMenuOpen = false;

  constructor(
    private readonly userService: UserService,
    private readonly authService: AuthenticationService,
    private readonly communicationService: CommunicationService,
    private readonly router: Router,
    private readonly toggleThemeService: ToggleThemeService
  ) {}

  ngOnInit(): void {
    this.setUserProfilePhoto();

    this.communicationService.action$.subscribe((action) => {
      if (action === WhichAction.UPDATE_NAVBAR_PROFILE_PHOTO) {
        this.setUserProfilePhoto();
      }
    });

    this.authenticatedUserId = this.authService.getNameIdentifierFromToken(
      localStorage.getItem('token')!
    )!;
  }

  toggleProfileMenu() {
    this.profileMenuOpen = !this.profileMenuOpen;
  }

  isUserAdmin(): boolean {
    return (
      this.authService.getUserRoleFromToken(localStorage.getItem('token')!) ===
      'Administrator'
    );
  }

  setUserProfilePhoto() {
    const userId: string | null = this.authService.getNameIdentifierFromToken(
      localStorage.getItem('token')!
    );

    this.userService
      .getUserProfilePhoto(userId!)
      .subscribe((response: PictureOut) => {
        this.userProfilePhoto = response;
        console.log(this.userProfilePhoto.filePath);
      });
  }

  logoutUser() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  getAuthenticatedUserId() {
    return this.authenticatedUserId;
  }

  toggleTheme() {
    const theme = this.toggleThemeService.getStoredTheme();
    if (theme === AppTheme.LIGHT) {
      this.toggleThemeService.setDarkTheme();
      this.isDarkMode = false;
    }

    if (theme === AppTheme.DARK) {
      this.toggleThemeService.setLightTheme();
      this.isDarkMode = true;
    }
  }
}
