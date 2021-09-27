import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../../_services/authentication.service'
import { UsersService } from 'src/app/_services/users.service';
import { ProfileService } from 'src/app/_services/profile.service';

import { strings } from '../../../constants/strings';
import { scrollTo } from 'src/app/_helpers/scrollTo';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnInit {
  isAuthenticated: boolean;
  public get isAuthenticatedValue(): boolean {
    return this.isAuthenticated;
  }
  public userName: string;
  public selectedProfileName: string;

  public navButtonsStrings = strings.navigationButtons;

  constructor(
    private authenticationService: AuthenticationService,
    private usersService: UsersService,
    private profileService: ProfileService) { }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated)
    {
      this.userName = this.usersService.currentUserValue.userName;
      this.getSelectedProfileName();
    }
  }

  getSelectedProfileName() {
    this.profileService.profileChangedObservable.subscribe(res => {
      if (res != null)
        this.selectedProfileName = res.profileName;
    });
  }

  scrollTo = scrollTo;
}
