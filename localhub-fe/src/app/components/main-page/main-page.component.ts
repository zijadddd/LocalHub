import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { PostOut } from '../../shared/models/post.model';
import { PostService } from '../../shared/services/post.service';
import { NgClass, NgFor, NgIf } from '@angular/common';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';
import { RouterModule } from '@angular/router';
import { UserLikedPostOut } from '../../shared/models/user-like.model';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { TruncatePipe } from '../../shared/core/truncate.pipe';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [
    NgFor,
    NgIf,
    NavigationBarComponent,
    RouterModule,
    NgClass,
    TruncatePipe,
  ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css',
})
export class MainPageComponent implements OnInit {
  public posts: PostOut[] = [];
  public didUserLikePost: Record<string, UserLikedPostOut> = {};
  public authenticatedUserId: string;

  constructor(
    private readonly titleService: Title,
    private readonly postService: PostService,
    private readonly authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('LocalHub - Posts');

    this.postService.getPosts().subscribe((response: PostOut[]) => {
      this.posts = response.map((post) => {
        post.created = this.dateTimeFormatter(post.created);
        post.updated =
          post.updated !== '0001-01-01T00:00:00'
            ? this.dateTimeFormatter(post.updated)
            : '';

        this.authenticatedUserId = this.authService.getNameIdentifierFromToken(
          localStorage.getItem('token')!
        )!;

        this.postService
          .didUserLikePost(this.authenticatedUserId, post.id)
          .subscribe((response) => {
            this.didUserLikePost[post.id] = response;
          });
        return post;
      });
    });
  }

  dateTimeFormatter(dateTime: string): string {
    const [date, timeWithSeconds] = dateTime.split('T');
    const [hour, minute] = timeWithSeconds.split(':');

    return date + ' ' + `${hour}:${minute}`;
  }

  likePost(postId: string, event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.postService
      .likePost(this.authenticatedUserId, postId)
      .subscribe((response) => {
        this.didUserLikePost[postId] = response;
        this.posts.find((post) => post.id === postId)!.likes =
          response.numberOfLikes;
      });
  }
}
