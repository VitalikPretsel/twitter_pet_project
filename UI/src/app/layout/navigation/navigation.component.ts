import { Component, OnInit } from '@angular/core';

import { strings } from '../../../constants/strings';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnInit {
  
  public navButtonsStrings = strings.navigationButtons;

  constructor() { }

  ngOnInit(): void {
  }

}
