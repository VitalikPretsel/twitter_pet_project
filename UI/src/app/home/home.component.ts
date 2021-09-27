import { Component, OnInit } from '@angular/core';

import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';
import { AuthenticationService } from '../_services/authentication.service';

import { User } from '../_models/user';

import { strings } from '../../constants/strings';
import { scrollTo } from '../_helpers/scrollTo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  public isAuthenticated: boolean;
  public user: User;
  profileIds: Array<number>;

  public homeStrings = strings.home;

  constructor(
    private profileService: ProfileService,
    private userService: UsersService,
    private authenticationService: AuthenticationService
  ) { }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated)
    {
      this.user = this.userService.currentUserValue;
      this.getSelectedProfile();
    }
  }

  getSelectedProfile() {
    this.profileService.profileChangedObservable.subscribe((res) => {
      if (res != null) {
        this.getFollowingsIds(res.id);
      }
    });
  }

  getFollowingsIds(profileId) {
    this.profileService.getFollowings(profileId)
      .subscribe(res => {
        this.profileIds = res;
      });
  }

  scrollTo = scrollTo;
}
