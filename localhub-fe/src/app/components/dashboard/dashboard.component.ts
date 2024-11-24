import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { UsersPageComponent } from './components/users-page/users-page.component';
import { PostsPageComponent } from './components/posts-page/posts-page.component';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [UsersPageComponent, PostsPageComponent, NgIf],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  ifUsersPage: boolean = true;

  constructor(private readonly titleService: Title) {}
  ngOnInit(): void {
    this.titleService.setTitle('LocalHub - Dashboard');
  }

  togglePages(temp: boolean) {
    this.ifUsersPage = temp;
  }
}
