import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProfileService } from '../_services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.sass']
})
export class ProfileComponent implements OnInit {

  profile: any;

  constructor(private service: ProfileService, private activatedRoute: ActivatedRoute) { 
    let profileName = this.activatedRoute.snapshot.paramMap.get('profileName');
    this.getProfile(profileName);
  }

  ngOnInit(): void {
  }

  getProfile(profileName) {
    this.service.getProfile(profileName).subscribe(data => {
        this.profile = data;
        this.getFollowersAmount(this.profile.id);
        this.getFollowingsAmount(this.profile.id);
        this.getPostsAmount(this.profile.id)
      });
  }

  getFollowersAmount(profileId) {
    this.service.getFollowersAmount(profileId).subscribe(data => {
      this.profile.followersAmount = data;
    });
  }

  getFollowingsAmount(profileId) {
    this.service.getFollowingsAmount(profileId).subscribe(data => {
      this.profile.followingsAmount = data;
    });
  }

  getPostsAmount(profileId) {
    this.service.getPostsAmount(profileId).subscribe(data => {
      this.profile.postsAmount = data;
    });
  }
}
