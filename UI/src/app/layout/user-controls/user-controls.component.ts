import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../_services/authentication.service'
import { ProfileService } from '../../_services/profile.service';
import { UsersService } from '../../_services/users.service';
import { MatDialog } from '@angular/material/dialog';

import { User } from '../../_models/user';
import { Profile } from '../../_models/profile';
import { CreateProfileComponent } from 'src/app/create-profile/create-profile.component';

@Component({
  selector: 'app-user-controls',
  templateUrl: './user-controls.component.html',
  styleUrls: ['./user-controls.component.sass']
})
export class UserControlsComponent implements OnInit {
  public user: User;
  public profiles: Profile[];
  public selectedProfileId: Number;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService,
    private profileService: ProfileService,
    private usersService: UsersService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.user = this.usersService.currentUserValue;
    this.getUserProfiles(this.user.id);
    this.getSelectedProfileId();
  }

  getUserProfiles(userId) {
    this.usersService.getUserProfiles(userId)
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

  openDialog() {
    const dialogRef = this.dialog.open(CreateProfileComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  logOut() {
    this.authenticationService.logout().subscribe(() => {
      this.router.navigate(['']);
    });
  }
}
