import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {
  public users: any;
  constructor(private service: UsersService) { }

  ngOnInit() {
    this.service.getUsers().pipe(first()).subscribe(response => {
      this.users = response;
    });
  }
}
