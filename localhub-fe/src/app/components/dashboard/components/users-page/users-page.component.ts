import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../shared/services/user.service';
import { UserOut } from '../../../../shared/models/user.model';
import { NgFor } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-users-page',
  standalone: true,
  imports: [NgFor, RouterModule],
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
  deleteUser(userId: string) {
    this.userService.deleteUser(userId).subscribe((response) => {
      this.users = this.users.filter((user) => user.id !== userId);
      const alert: Alert = new Alert();
      alert.isWarning = false;
      alert.message = response.message;
      this.communicationService.showAlertPopup(
        WhichAction.SHOW_ALERT_POPUP,
        alert
      );
    });
  }
}
