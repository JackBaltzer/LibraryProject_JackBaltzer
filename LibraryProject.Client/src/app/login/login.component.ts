import { ReturnStatement, ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '../_services/authentication.service';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {

  }

  login(): void {
    this.error = '';
    this.loading = true;
    this.authenticationService.login(this.email, this.password)
      .subscribe({
        next: (r) => {
          // console.log('login next', r);
          // // get return url from route parameters or default to '/'
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: error => {
          console.log('login error ', error);
          if (error == 'Unauthorized' || error == 'Bad Request') {

            this.error = 'Forkert brugernavn eller kodeord';
          }
          else {
            this.error = error;
          }
          this.loading = false;
        }
      });
  }

}
