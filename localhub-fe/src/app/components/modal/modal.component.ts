import { NgClass, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../../shared/services/communication.service';
import { Modal } from '../../shared/models/modal.model';
import { WhichAction } from '../../shared/models/which-action.model';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [NgIf, NgClass],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css',
})
export class ModalComponent implements OnInit {
  public modal: Modal;

  constructor(private readonly communicationService: CommunicationService) {}
  ngOnInit(): void {
    this.communicationService.data$.subscribe((response) => {
      if (response.action === WhichAction.OPEN_MODAL)
        this.modal = response.data;
    });
  }

  toggleIsModalShowed() {
    this.modal.isShowed = !this.modal.isShowed;
    this.communicationService.isModalConfirmed(
      WhichAction.IS_MODAL_CONFIRMED,
      false
    );
  }

  confirm() {
    this.modal.isShowed = !this.modal.isShowed;
    this.communicationService.isModalConfirmed(
      WhichAction.IS_MODAL_CONFIRMED,
      true
    );
  }
}
