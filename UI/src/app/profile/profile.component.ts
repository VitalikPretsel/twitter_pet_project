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
    console.log(this.profile);
  }

  ngOnInit(): void {
  }

  getProfile(profileName) {
    this.service.getProfile(profileName).subscribe(data => {
        this.profile = data;
      });
  }

}
