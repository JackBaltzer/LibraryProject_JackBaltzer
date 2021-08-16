import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthenticationService } from '../_services/authentication.service';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  submitted = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    // console.log(this.authenticationService.currentUserValue);
    if (this.authenticationService.currentUserValue != null && this.authenticationService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {

  }

  login(): void {
    this.error = '';
    this.authenticationService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // // get return url from route parameters or default to '/'
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: obj => {
          // console.log('login error ', obj.error);
          if (obj.error.status == 400 || obj.error.status == 401 || obj.error.status == 500) {
            this.error = 'Forkert brugernavn eller kodeord';
          }
          else {
            this.error = obj.error.title;
          }
        }
      });
  }
}
