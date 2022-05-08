import {Component, Input, OnInit} from '@angular/core';
import {QuestionTypes} from "../../../../core/models/question-types";
import {AbstractControl, Form, FormGroup} from "@angular/forms";
import {FormGroupService} from "../../../../core/services/form-group.service";

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

  @Input()
  public questionControl: AbstractControl = new FormGroup({});

  constructor(public formGroupService: FormGroupService) { }

  ngOnInit(): void {
  }

  public selectCorrectAnswer(correctAnswer: string) {
    this.correctAnswer = correctAnswer;
  }

  public selectQuestionName(questionName: string) {
    this.questionName = questionName;
  }
}
