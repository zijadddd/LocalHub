import { Injectable } from '@angular/core';
import { WhichAction } from '../models/which-action.model';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommunicationService {
  private actionSource = new Subject<WhichAction>();
  action$ = this.actionSource.asObservable();

  updateNavbarProfilePhoto() {
    this.actionSource.next(WhichAction.UPDATE_NAVBAR_PROFILE_PHOTO);
  }
}
