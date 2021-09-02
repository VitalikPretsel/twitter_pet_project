import { Component, OnInit } from '@angular/core';

import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';

import { User } from '../_models/user';

import { strings } from '../../constants/strings';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  public user: User;
  profileIds: Array<number>;

  public homeStrings = strings.home;

  constructor(
    private profileService: ProfileService,
    private userService: UsersService
  ) { }

  ngOnInit() {
    this.getCurrentUser();
    this.getSelectedProfile();
  }

  getCurrentUser() {
    this.userService.getCurrentUser()
      .subscribe(res => {
        this.user = res;
      });
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
}
