import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private http: HttpClient) {}

  login() {
    const credentials = {
      username: this.username,
      password: this.password
    };

    this.http.post<any>('http://localhost:5000/auth/login', credentials)
      .subscribe(
        (response) => {
          // Handle successful login
          console.log('Login successful', response);
        },
        (error) => {
          // Handle login error
          console.error('Login failed', error);
        }
      );
  }
}
