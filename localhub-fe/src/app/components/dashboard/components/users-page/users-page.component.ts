import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../shared/services/user.service';
import { UserOut } from '../../../../shared/models/user.model';
import { NgClass, NgFor } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CommunicationService } from '../../../../shared/services/communication.service';
import { WhichAction } from '../../../../shared/models/which-action.model';
import { Alert } from '../../../../shared/models/alert.model';
import { AuthenticationService } from '../../../../shared/services/authentication.service';
import { SuspendIn } from '../../../../shared/models/suspend.model';
import { Modal } from '../../../../shared/models/modal.model';

@Component({
  selector: 'app-users-page',
  standalone: true,
  imports: [NgFor, RouterModule, NgClass],
  templateUrl: './users-page.component.html',
  styleUrl: './users-page.component.css',
})
export class UsersPageComponent implements OnInit {
  public users: UserOut[] = [];
  public suspendedUsers: Record<string, boolean> = {};
  private userIdForDeletingOrSuspending: string;
  public authenticatedUserId: string = '';

  constructor(
    private readonly userService: UserService,
    private readonly communicationService: CommunicationService,
    private readonly authenticationService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((response) => {
      this.users = response;

      this.users.forEach((user) => {
        this.authenticationService
          .isSuspended(user.id)
          .subscribe((response) => {
            if (response.isSuspended === true) {
              this.suspendedUsers[user.id] = true;
            }

            if (response.isSuspended === false) {
              this.suspendedUsers[user.id] = false;
            }
          });
      });

      this.communicationService.data$.subscribe((response) => {
        if (response.data[0] === true && response.data[1] === undefined)
          this.deleteUser(this.userIdForDeletingOrSuspending);
      });

      this.communicationService.data$.subscribe((response) => {
        if (response.data[0] === true && response.data[1] !== undefined) {
          const suspendReason: SuspendIn = new SuspendIn();
          suspendReason.reason = response.data[1];
          this.suspendUser(this.userIdForDeletingOrSuspending, suspendReason);
        }
      });
    });

    this.authenticatedUserId =
      this.authenticationService.getNameIdentifierFromToken(
        localStorage.getItem('token')!
      )!;
  }

  dateTimeFormatter(dateTime: string): string {
    const [date, timeWithSeconds] = dateTime.split('T');
    const [hour, minute] = timeWithSeconds.split(':');

    return date + ' ' + `${hour}:${minute}`;
  }

  openDeleteSuspendingUserModal(id: string, action: string, event: Event) {
    event.preventDefault();
    event.stopPropagation();

    const deleteModal: Modal = new Modal();
    deleteModal.heading = 'Are you sure?';
    deleteModal.content = `This action CANNOT be undone. This will permanently ${action} the user.`;
    deleteModal.isWarning = true;
    deleteModal.isShowed = true;
    deleteModal.needInput = action === 'suspend' ? true : false;

    this.communicationService.openModal(WhichAction.OPEN_MODAL, deleteModal);
    this.userIdForDeletingOrSuspending = id;
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

  suspendUser(userId: string, suspendReason: SuspendIn) {
    this.authenticationService
      .suspendUser(userId, suspendReason)
      .subscribe((response) => {
        const alert: Alert = new Alert();
        alert.isWarning = false;
        alert.message = response.message;
        this.communicationService.showAlertPopup(
          WhichAction.SHOW_ALERT_POPUP,
          alert
        );

        this.suspendedUsers[userId] = true;
      });
  }
}
