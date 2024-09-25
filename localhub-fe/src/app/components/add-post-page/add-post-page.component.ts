import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { PostService } from '../../shared/services/post.service';
import { NgIf } from '@angular/common';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { CommunicationService } from '../../shared/services/communication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-post-page',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './add-post-page.component.html',
  styleUrl: './add-post-page.component.css',
})
export class AddPostPageComponent implements OnInit {
  public postForm: FormGroup;
  public selectedFile: File | null = null;

  constructor(
    private readonly postService: PostService,
    private readonly authService: AuthenticationService,
    private readonly communicationService: CommunicationService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.postForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      image: new FormControl(null),
    });
  }

  onSubmit() {
    if (this.postForm.valid) {
      const userId = this.authService.getNameIdentifierFromToken(
        localStorage.getItem('token')!
      )!;

      const formData = new FormData();
      formData.append('Name', this.postForm.value.name);
      formData.append('Description', this.postForm.value.description);

      if (this.selectedFile) {
        formData.append('Image', this.selectedFile);
      }

      this.postService.createPost(userId, formData).subscribe(
        (response) => {
          this.router.navigate(['postPage', response.id]);
        },
        (error) => {
          console.error('Error occurred while creating post:', error);
        }
      );
    } else {
      console.log('Forma nije validna');
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.postForm.patchValue({ image: file });
    }
  }
}
