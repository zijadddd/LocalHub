import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { UsersPageComponent } from './components/users-page/users-page.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [UsersPageComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  constructor(private readonly titleService: Title) {}
  ngOnInit(): void {
    this.titleService.setTitle('LocalHub - Dashboard');
  }
}
