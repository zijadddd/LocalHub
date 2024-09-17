import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from '../../shared/services/authentication.service';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthIn, AuthOut } from '../../shared/models/auth-in.model';
import { NgClass, NgIf } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authentication-page',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, NgIf, NgClass],
  templateUrl: './authentication-page.component.html',
  styleUrl: './authentication-page.component.css',
})
export class AuthenticationPageComponent implements OnInit {
  public authenticateUserForm: FormGroup;
  public isAlertVisible: boolean = false;
  public alertMessage: string = '';

  constructor(
    private readonly titleService: Title,
    private readonly authService: AuthenticationService,
    private readonly formBuilder: FormBuilder,
    private readonly router: Router
  ) {}
  ngOnInit(): void {
    this.titleService.setTitle('LocalHub - Login Page');

    this.authenticateUserForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(64),
          Validators.pattern(
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
          ),
        ],
      ],
    });
  }

  authenticateUser() {
    const request = new AuthIn();
    request.email = this.authenticateUserForm.value.email;
    request.password = this.authenticateUserForm.value.password;

    this.authService.authenticateUser(request).subscribe({
      next: (response: AuthOut) => {
        localStorage.setItem('token', response.token);
        this.router.navigate(['/']);
      },
      error: (error: HttpErrorResponse) => {
        this.isAlertVisible = true;
        this.alertMessage = error.error[0].Message;
      },
    });
  }
}
