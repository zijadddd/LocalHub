import { NgClass, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PictureOut } from '../../shared/models/picture.model';
import { UserService } from '../../shared/services/user.service';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { Router, RouterModule } from '@angular/router';
import { CommunicationService } from '../../shared/services/communication.service';
import { WhichAction } from '../../shared/models/which-action.model';

@Component({
  selector: 'app-navigation-bar',
  standalone: true,
  imports: [NgClass, NgIf, NgFor, RouterModule],
  templateUrl: './navigation-bar.component.html',
  styleUrl: './navigation-bar.component.css',
})
export class NavigationBarComponent implements OnInit {
  public userProfilePhoto: PictureOut = new PictureOut();

  profileMenuOpen = false;

  constructor(
    private readonly userService: UserService,
    private readonly authService: AuthenticationService,
    private readonly communicationService: CommunicationService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.setUserProfilePhoto();

    this.communicationService.action$.subscribe((action) => {
      if (action === WhichAction.UPDATE_NAVBAR_PROFILE_PHOTO) {
        this.setUserProfilePhoto();
      }
    });
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
}
