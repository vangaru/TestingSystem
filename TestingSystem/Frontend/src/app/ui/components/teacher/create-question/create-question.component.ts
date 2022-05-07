import {Component, Input, OnInit} from '@angular/core';
import {QuestionTypes} from "../../../../core/models/QuestionTypes";

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})
export class CreateQuestionComponent implements OnInit {

  public checkBoxQuestionType = QuestionTypes.CheckboxQuestion;
  public radioButtonQuestionType = QuestionTypes.RadiobuttonQuestion;
  public stringQuestionType = QuestionTypes.StringQuestion;

  @Input()
  public questionType: string = QuestionTypes.StringQuestion;

  constructor() { }

  ngOnInit(): void {
  }

}
