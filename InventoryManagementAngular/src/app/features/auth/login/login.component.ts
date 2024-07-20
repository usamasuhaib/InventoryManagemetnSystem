import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ ReactiveFormsModule,],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  imagePath: string;
  
  email: string = '';
  password: string = '';
  loginForm!: FormGroup;


  serverMessage: string | null = null;
  jwtToken: string | null = null;


  constructor(private router:Router, private title:Title, private formBuilder:FormBuilder,private  authService :AuthService, private activeRoute:ActivatedRoute, private toaster:ToastrService){
    this.imagePath = 'assets/images/erplogo.png';

  }


  ngOnInit(){
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
    this.title.setTitle(`User Login | School Management System`)

  }

  onClickLogin() {
    if (this.loginForm.valid) {
      this.authService.onLogin(this.loginForm.value).subscribe({
        next: (res) => {
          this.authService.storeToken(res.token); // Store token
          this.toaster.success('Login successful!');
          this.loginForm.reset();
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          this.toaster.error(err?.error?.msg || 'Login failed. Please try again.');
        }
      });
    } else {
      this.toaster.error('Invalid Credentials');
    }
  }





  onForgotPassword(){
    return this.router.navigate(['forgot-password'])
  }
}
