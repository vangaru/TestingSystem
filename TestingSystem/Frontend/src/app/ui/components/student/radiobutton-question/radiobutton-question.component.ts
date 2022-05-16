import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";
import {AnswerQuestionModel} from "../../../../core/models/answer-question-model";

@Component({
  selector: 'app-radiobutton-question',
  templateUrl: './radiobutton-question.component.html',
  styleUrls: ['./radiobutton-question.component.css']
})
export class RadiobuttonQuestionComponent implements OnInit {

  public questionAnswer: string = "";

  @Input()
  public question?: TakeQuestionModel;

  @Output()
  public answer: EventEmitter<AnswerQuestionModel> = new EventEmitter<AnswerQuestionModel>();

  public uuid: string = v4();

  constructor() { }

  ngOnInit(): void {
  }

  public onAnswer() {
    const answerModel: AnswerQuestionModel = new AnswerQuestionModel(this.question?.questionId, this.questionAnswer);
    this.answer.emit(answerModel);
  }
}
