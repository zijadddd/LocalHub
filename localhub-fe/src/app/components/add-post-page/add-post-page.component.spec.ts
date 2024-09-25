import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPostPageComponent } from './add-post-page.component';

describe('AddPostPageComponent', () => {
  let component: AddPostPageComponent;
  let fixture: ComponentFixture<AddPostPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPostPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPostPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
