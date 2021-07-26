import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass']
})
export class UsersComponent implements OnInit {
  users: any;
  nonAuthorized: boolean = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get("https://localhost:44347/api/users").subscribe(response => {
      this.users = response;
    }, err => {
      this.nonAuthorized = true;
      console.log(err)
    });
  }

}
