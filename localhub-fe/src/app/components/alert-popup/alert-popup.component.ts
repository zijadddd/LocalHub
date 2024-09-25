import { NgClass, NgIf } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Alert } from '../../shared/models/alert.model';
import { CommunicationService } from '../../shared/services/communication.service';
import { WhichAction } from '../../shared/models/which-action.model';

@Component({
  selector: 'app-alert-popup',
  standalone: true,
  imports: [NgClass, NgIf],
  templateUrl: './alert-popup.component.html',
  styleUrl: './alert-popup.component.css',
})
export class AlertPopupComponent implements OnInit {
  public isShowed: boolean = false;
  public alert: Alert;

  constructor(
    private readonly ref: ChangeDetectorRef,
    private readonly communicationService: CommunicationService
  ) {}

  ngOnInit(): void {
    this.communicationService.data$.subscribe((response) => {
      if (response.action === WhichAction.SHOW_ALERT_POPUP) {
        this.alert = response.data;
        this.isShowed = true;
        this.ref.detectChanges();

        setTimeout(() => {
          this.isShowed = false;
          this.ref.detectChanges();
        }, 3500);
      }
    });

    this.ref.detectChanges();
  }
}
