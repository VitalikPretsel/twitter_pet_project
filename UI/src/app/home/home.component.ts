import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '../_models/user';
import { AuthenticationService } from '../_services/authentication.service'
import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  private isAuthenticated: boolean;
  public user: User;
  public get isAuthenticatedValue(): boolean {
    return this.isAuthenticated;
  }

  profileIds: Array<number>;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private profileService: ProfileService,
    private userService: UsersService
  ) { }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated)
    {
      this.getCurrentUser();
      this.getFollowingsIds(3);
    }
  }

  getCurrentUser() {
    this.userService.getCurrentUser()
      .subscribe(res => {
        this.user = res;
      });
  }

  getFollowingsIds(profileId) {
    this.profileService.getFollowings(profileId)
      .subscribe(res => {
        this.profileIds = res;
      })
  }

  logOut() {
    this.authenticationService.logout().subscribe(res => {
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
    });
  }
}
