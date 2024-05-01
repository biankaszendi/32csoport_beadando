import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  username: string = '';
  firstName: string = '';
  lastName: string = '';
  password: string = '';
  confirmedPassword: string = '';

  constructor(private http: HttpClient) {}

  register() {
    const newUser = {
      username: this.username,
      firstName: this.firstName,
      lastName: this.lastName,
      password: this.password,
      confirmedPassword: this.confirmedPassword
    };

    this.http.post<any>('http://localhost:5000/auth/register', newUser)
      .subscribe(
        (response) => {
          // Handle successful registration
          console.log('Registration successful', response);
        },
        (error) => {
          // Handle registration error
          console.error('Registration failed', error);
        }
      );
  }
}
