import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from '../../_services/authentication.service'
import { UsersService } from 'src/app/_services/users.service';

import { strings } from '../../../constants/strings';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.sass']
})
export class NavigationComponent implements OnInit {
  public isAuthenticated: boolean;
  public get isAuthenticatedValue(): boolean {
    return this.isAuthenticated;
  }
  public userName: string;

  public navButtonsStrings = strings.navigationButtons;

  constructor(
    private authenticationService: AuthenticationService,
    private usersService: UsersService) { }

  async ngOnInit() {
    this.isAuthenticated = await this.authenticationService.isAuthenticated().toPromise();
    if (this.isAuthenticated)
      this.getCurrentUserName();
  }

  getCurrentUserName() {
    this.usersService.getCurrentUserName()
      .subscribe(res => {
        this.userName = res;
      });
  }
}
