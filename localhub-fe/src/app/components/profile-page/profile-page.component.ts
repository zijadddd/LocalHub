import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { UserOut } from '../../shared/models/user.model';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { NgIf } from '@angular/common';
import { PostService } from '../../shared/services/post.service';
import { LikeAndCommentCountOut } from '../../shared/models/like-comment.model';
import { PictureIn } from '../../shared/models/picture.model';
import { CommunicationService } from '../../shared/services/communication.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [NgIf],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css',
})
export class ProfilePageComponent implements OnInit {
  constructor(
    private readonly userService: UserService,
    private readonly authService: AuthenticationService,
    private readonly communicationService: CommunicationService,
    private readonly postService: PostService,
    private readonly route: ActivatedRoute
  ) {}

  public user: UserOut;
  public showBanAndDeleteBtn: boolean = false;
  public likeAndCommentCount: LikeAndCommentCountOut;

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id')!;

    this.userService.getUser(userId).subscribe((response: UserOut) => {
      this.user = response;

      this.userService.getUserRole(this.user.id).subscribe((response) => {
        const authenticatedUserRole = this.authService.getUserRoleFromToken(
          localStorage.getItem('token')!
        );
        if (authenticatedUserRole === 'Administrator')
          this.showBanAndDeleteBtn = true;

        const authenticatedUserNameIdentifier =
          this.authService.getNameIdentifierFromToken(
            localStorage.getItem('token')!
          );
        if (authenticatedUserNameIdentifier === this.user.id) {
          this.showBanAndDeleteBtn = false;
        }
      });

      this.postService
        .getUserLikeAndCommentCount(this.user.id)
        .subscribe((response) => {
          this.likeAndCommentCount = response;
        });
    });
  }

  getUserAge(): number {
    const yearFromString = new Date(this.user.birthDate).getFullYear();
    const currentYear = new Date().getFullYear();
    const difference = currentYear - yearFromString;

    return difference;
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];

      const formData = new FormData();
      formData.append('image', file);

      this.userService
        .changeProfilePhoto(this.user.id, formData)
        .subscribe((response) => {
          this.user.profilePhoto = response.filePath;
          this.communicationService.updateNavbarProfilePhoto();
        });
    }
  }
}
