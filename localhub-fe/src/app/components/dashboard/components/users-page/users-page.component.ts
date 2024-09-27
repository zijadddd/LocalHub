import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../shared/services/user.service';
import { UserOut } from '../../../../shared/models/user.model';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-users-page',
  standalone: true,
  imports: [NgFor],
  templateUrl: './users-page.component.html',
  styleUrl: './users-page.component.css',
})
export class UsersPageComponent implements OnInit {
  public users: UserOut[] = [];

  constructor(private readonly userService: UserService) {}
  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;
    });
  }
}
