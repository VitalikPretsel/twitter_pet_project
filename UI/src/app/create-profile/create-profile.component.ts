import { Component, OnInit } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { MatDialogRef } from '@angular/material/dialog';

import { ProfileService } from '../_services/profile.service';
import { UsersService } from '../_services/users.service';

import { strings } from 'src/constants/strings';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.sass']
})
export class CreateProfileComponent implements OnInit {

  public createProfileStrings = strings.createProfile;

  private id: Number;

  constructor(
    private profileService: ProfileService,
    private usersService: UsersService,
    public dialogRef: MatDialogRef<CreateProfileComponent>
  ) { }

  ngOnInit(): void {
  }

  createProfile(createProfileForm: NgForm) {
    console.log("what?");
    this.usersService.getCurrentUser().subscribe(res => {
      this.id = res.id;
      createProfileForm.form.patchValue({ userId: this.id });
      this.profileService.createProfile(createProfileForm.value).subscribe(res => {
        this.closeDialog();
      });
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  goBack(stepper: MatStepper) {
    stepper.previous();
  }

  goForward(stepper: MatStepper) {
    stepper.next();
  }
}
