import { Component, OnInit } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.sass']
})
export class CreateProfileComponent implements OnInit {

  public createProfileStrings = strings.createProfile;

  constructor() { }

  ngOnInit(): void {
  }

  goBack(stepper: MatStepper) {
    stepper.previous();
  }

  goForward(stepper: MatStepper) {
    stepper.next();
  }
}
