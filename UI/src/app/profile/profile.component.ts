import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';
import { AuthenticationService } from '../_services/authentication.service';

import { User } from '../_models/user';
import { Profile } from '../_models/profile';

import { strings } from 'src/constants/strings';
import { scrollTo } from '../_helpers/scrollTo';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.sass']
})
export class ProfileComponent implements OnInit {
  public isAuthenticated: boolean;
  public isCurrentUserOwner: boolean;
  public user: User;

  profile: Profile;
  profileIds: Array<number>;

  public profileStrings = strings.profile;

  constructor(
    private service: ProfileService,
    private usersService: UsersService,
    private authenticationService: AuthenticationService,
    private activatedRoute: ActivatedRoute) { 
    let profileName = this.activatedRoute.snapshot.paramMap.get('profileName');
    this.getProfile(profileName);
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated)
      this.getCurrentUser();
  }

  getCurrentUser() {
    this.usersService.getCurrentUser()
      .subscribe(res => {
        this.user = res;
        this.isCurrentUserOwner = this.user.id == this.profile.userId;
      });
  }

  getProfile(profileName) {
    this.service.getProfile(profileName).subscribe(data => {
        this.profile = data;
        this.profileIds = [this.profile.id];
      });
  }

  scrollTo = scrollTo;
}
