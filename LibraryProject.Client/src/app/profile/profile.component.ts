import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: []
})
export class ProfileComponent implements OnInit {

  currentUser: User = { id: 0, email: '', username: '' };

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // get the current user from authentication service
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit() {

  }

}
