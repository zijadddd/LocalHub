import { Injectable } from '@angular/core';
import { WhichAction } from '../models/which-action.model';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommunicationService {
  private actionSource = new Subject<WhichAction>();
  action$ = this.actionSource.asObservable();

  private dataSource = new Subject<any>();
  data$ = this.dataSource.asObservable();

  updateNavbarProfilePhoto() {
    this.actionSource.next(WhichAction.UPDATE_NAVBAR_PROFILE_PHOTO);
  }

  openModal(data: any) {
    this.dataSource.next(data);
  }

  isModalConfirmed(data: boolean) {
    this.dataSource.next(data);
  }
}
