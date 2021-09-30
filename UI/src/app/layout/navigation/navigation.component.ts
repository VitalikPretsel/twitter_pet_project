import { Component, OnInit } from '@angular/core';

import { UsersService } from 'src/app/_services/users.service';
import { ProfileService } from 'src/app/_services/profile.service';

import { strings } from '../../../constants/strings';
import { scrollTo } from 'src/app/_helpers/scrollTo';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnInit {
  public user: User;
  public selectedProfileName: string;

  public navButtonsStrings = strings.navigationButtons;

  constructor(
    private usersService: UsersService,
    private profileService: ProfileService) { }

  ngOnInit() {
    this.user = this.usersService.currentUserValue;
    if (this.user)
    {
      this.getSelectedProfileName();
    }
  }

  getSelectedProfileName() {
    this.profileService.currentProfileObservable.subscribe(res => {
      if (res != null)
        this.selectedProfileName = res.profileName;
    });
  }

  scrollTo = scrollTo;
}
