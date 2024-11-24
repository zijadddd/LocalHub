import { Component, OnInit } from '@angular/core';
import { PostService } from '../../../../shared/services/post.service';
import { NgFor } from '@angular/common';
import { PostOut } from '../../../../shared/models/post.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-posts-page',
  standalone: true,
  imports: [NgFor, RouterModule],
  templateUrl: './posts-page.component.html',
  styleUrl: './posts-page.component.css',
})
export class PostsPageComponent implements OnInit {
  public posts: PostOut[] = [];

  constructor(private readonly postService: PostService) {}
  ngOnInit(): void {
    this.postService.getPosts().subscribe((response) => {
      this.posts = response;
    });
  }

  dateTimeFormatter(dateTime: string): string {
    const [date, timeWithSeconds] = dateTime.split('T');
    const [hour, minute] = timeWithSeconds.split(':');

    return date + ' ' + `${hour}:${minute}`;
  }
}
