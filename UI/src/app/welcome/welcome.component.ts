import { Component, OnInit } from '@angular/core';

import { strings } from 'src/constants/strings';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.sass']
})
export class WelcomeComponent implements OnInit {

  public welcomeStrings = strings.welcome;

  constructor() {}

  ngOnInit(): void {
  }

}
