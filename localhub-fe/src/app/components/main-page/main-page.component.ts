import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { PostOut } from '../../shared/models/post.model';
import { PostService } from '../../shared/services/post.service';
import { NgFor, NgIf } from '@angular/common';
import { NavigationBarComponent } from '../navigation-bar/navigation-bar.component';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [NgFor, NgIf, NavigationBarComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css',
})
export class MainPageComponent implements OnInit {
  public posts: PostOut[] = [];

  constructor(
    private readonly titleService: Title,
    private readonly postService: PostService
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle('LocalHub');

    this.postService.getPosts().subscribe((response: PostOut[]) => {
      this.posts = response.map((post) => {
        post.created = this.dateTimeFormatter(post.created);
        post.updated =
          post.updated !== '0001-01-01T00:00:00'
            ? this.dateTimeFormatter(post.updated)
            : '';
        return post;
      });
    });
  }

  dateTimeFormatter(dateTime: string): string {
    const [date, timeWithSeconds] = dateTime.split('T');
    const [hour, minute] = timeWithSeconds.split(':');

    return date + ' ' + `${hour}:${minute}`;
  }
}
