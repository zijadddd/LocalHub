import { NgClass, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../../shared/services/communication.service';
import { Modal } from '../../shared/models/modal.model';

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
    this.communicationService.data$.subscribe((data) => {
      this.modal = data;
    });
  }

  toggleIsModalShowed() {
    this.modal.isShowed = !this.modal.isShowed;
    this.communicationService.isModalConfirmed(false);
  }

  confirm() {
    this.modal.isShowed = !this.modal.isShowed;
    this.communicationService.isModalConfirmed(true);
  }
}
