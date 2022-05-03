import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../../../core/services/account.service";

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  public studentNames: string[] = [];
  public selectedStudents: string[] = [];

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.refreshStudents();
  }

  private refreshStudents() {
    this.accountService.getStudentNames().subscribe(
      studentNames => {
        this.studentNames = [...studentNames];
      }
    );
  }
}
