import {Component, Input, OnInit} from '@angular/core';
import {QuestionTypes} from "../../../../core/models/question-types";

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})
export class CreateQuestionComponent implements OnInit {

  public checkBoxQuestionType = QuestionTypes.CheckboxQuestion;
  public radioButtonQuestionType = QuestionTypes.RadiobuttonQuestion;
  public stringQuestionType = QuestionTypes.StringQuestion;

  private correctAnswer?: string;
  private questionName?: string;

  @Input()
  public questionType: string = QuestionTypes.StringQuestion;

  constructor() { }

  ngOnInit(): void {
  }

  public selectCorrectAnswer(correctAnswer: string) {
    this.correctAnswer = correctAnswer;
  }

  public selectQuestionName(questionName: string) {
    this.questionName = questionName;
  }

}
