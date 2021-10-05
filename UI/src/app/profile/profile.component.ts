import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';
import { FollowingService } from '../_services/following.service';
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
  public isFollowing: boolean;
  public isCurrentUserOwner: boolean;
  public user: User;

  profile: Profile;
  profileIds: Array<number>;

  public profileStrings = strings.profile;

  constructor(
    private profileService: ProfileService,
    private usersService: UsersService,
    private followingService: FollowingService,
    private authenticationService: AuthenticationService,
    private activatedRoute: ActivatedRoute) {
  }

  async ngOnInit() {
    this.activatedRoute.params.subscribe(() => {
      let profileName = this.activatedRoute.snapshot.paramMap.get('profileName');
      this.getProfile(profileName);
    });
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated) {
      this.user = this.usersService.currentUserValue;
      this.isCurrentUserOwner = this.user.id == this.profile.userId;
      if (!this.isCurrentUserOwner) {
        this.getIsFollowing(this.profile.id);
      }
    }
  }

  getProfile(profileName) {
    this.profileService.getProfile(profileName).subscribe(data => {
      this.profile = data;
      this.profileIds = [this.profile.id];
    });
  }

  getIsFollowing(profileId) {
    this.profileService.currentProfileObservable.subscribe((res) => {
      if (res != null) {
        this.followingService.isfollowing(this.profileService.currentProfileValue.id, profileId)
          .subscribe(res => {
            this.isFollowing = res;
          });
      }
    });
  }

  follow(profileId) {
    this.followingService.follow({
      followerProfileId: this.profileService.currentProfileValue.id,
      followingProfileId: profileId
    }).subscribe(res => {
      this.isFollowing = true;
    });
  }

  unfollow(profileId) {
    this.followingService.unfollow(this.profileService.currentProfileValue.id, profileId).subscribe(res => {
      this.isFollowing = false;
    });
  }

  scrollTo = scrollTo;
}
