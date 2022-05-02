import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../../../core/services/account.service";
import {User} from "../../../../core/models/user";

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  public users: User[] = [];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.refreshUsers();
  }

  private refreshUsers() {
    this.accountService.getUsers().subscribe(users => {
      this.users = [...users];
    })
  }
}
