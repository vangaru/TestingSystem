import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../../../core/services/account.service";
import {QuestionTypes} from "../../../../core/models/QuestionTypes";

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  public studentNames: string[] = [];
  public checkBoxQuestionType = QuestionTypes.CheckboxQuestion;
  public radioButtonQuestionType = QuestionTypes.RadiobuttonQuestion;
  public stringQuestionType = QuestionTypes.StringQuestion;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.refreshStudentNames();
  }

  private refreshStudentNames() {
    this.accountService.getStudentNames().subscribe(
      studentNames => {
        this.studentNames = [...studentNames];
      }
    );
  }
}
