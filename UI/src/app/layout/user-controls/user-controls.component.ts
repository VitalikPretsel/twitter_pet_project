import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../_services/authentication.service'
import { ProfileService } from '../../_services/profile.service';
import { UsersService } from '../../_services/users.service';

import { User } from '../../_models/user';

@Component({
  selector: 'app-user-controls',
  templateUrl: './user-controls.component.html',
  styleUrls: ['./user-controls.component.sass']
})
export class UserControlsComponent implements OnInit {
  public user: User;
  public profiles: any;
  public selectedProfileId: Number;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private profileService: ProfileService,
    private userService: UsersService
  ) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser() {
    this.userService.getCurrentUser()
      .subscribe(res => {
        this.user = res;
        this.getUserProfiles(this.user.id);
        this.getSelectedProfileId();
      });
  }

  getUserProfiles(userId) {
    this.userService.getUserProfiles(userId)
      .subscribe(res => {
        this.profiles = res;
        this.selectProfile(this.profiles[0]);
      })
  }

  getSelectedProfileId() {
    this.profileService.profileChangedObservable.subscribe(res => {
      if (res != null)
        this.selectedProfileId = res.id;
    });
  }

  selectProfile(profile) {
    this.profileService.changeProfile(profile);
  }

  logOut() {
    this.authenticationService.logout().subscribe(res => {
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
    });
  }
}
