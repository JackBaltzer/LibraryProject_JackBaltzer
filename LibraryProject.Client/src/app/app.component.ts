import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services/authentication.service';
import { Role, User } from './models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentUser: User = { id: 0, email: '', username: '' };

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // get the current user from authentication service
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      // ask authentication service to perform logout
      this.authenticationService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authenticationService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }
  }
}
