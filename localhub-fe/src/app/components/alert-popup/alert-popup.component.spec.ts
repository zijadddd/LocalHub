import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertPopupComponent } from './alert-popup.component';

describe('AlertPopupComponent', () => {
  let component: AlertPopupComponent;
  let fixture: ComponentFixture<AlertPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AlertPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AlertPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
