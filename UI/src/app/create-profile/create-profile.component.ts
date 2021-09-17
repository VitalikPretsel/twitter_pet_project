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
  invalidUser: boolean;
  errors: string[];
  private id: Number;

  public createProfileStrings = strings.createProfile;

  constructor(
    private profileService: ProfileService,
    private usersService: UsersService,
    private dialogRef: MatDialogRef<CreateProfileComponent>
  ) { }

  ngOnInit(): void {
  }

  createProfile(createProfileForm: NgForm) {
    this.usersService.getCurrentUser().subscribe(res => {
      this.id = res.id;
      createProfileForm.form.patchValue({ userId: this.id });
      this.profileService.createProfile(createProfileForm.value).subscribe(res => {
        this.invalidUser = false;
        this.closeDialog();
      }, err => {
        this.invalidUser = true;
        this.errors = [];
        let errorObject;

        errorObject = err.error;

        for (var errorField in errorObject) {
          errorObject[errorField].forEach(element => {
            this.errors.push(errorField + ': ' + element);
          });
        }
      });
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  goBack(stepper: MatStepper) {
    this.invalidUser = false;
    stepper.previous();
  }

  goForward(stepper: MatStepper) {
    stepper.next();
  }
}
