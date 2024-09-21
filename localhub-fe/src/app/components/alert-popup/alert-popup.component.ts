import { NgClass } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-alert-popup',
  standalone: true,
  imports: [NgClass],
  templateUrl: './alert-popup.component.html',
  styleUrl: './alert-popup.component.css',
})
export class AlertPopupComponent implements OnInit {
  public isWarning: boolean = false;

  constructor(private readonly ref: ChangeDetectorRef) {}
  ngOnInit(): void {
    this.ref.detectChanges();
  }
}
