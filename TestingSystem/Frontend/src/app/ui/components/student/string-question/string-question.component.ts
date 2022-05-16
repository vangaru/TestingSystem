import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";
import {AnswerQuestionModel} from "../../../../core/models/answer-question-model";

@Component({
  selector: 'app-string-question',
  templateUrl: './string-question.component.html',
  styleUrls: ['./string-question.component.css']
})
export class StringQuestionComponent implements OnInit {

  @Input()
  public question?: TakeQuestionModel;

  @Output()
  public answer: EventEmitter<AnswerQuestionModel> = new EventEmitter<AnswerQuestionModel>();

  public uuid: string = v4();

  constructor() { }

  ngOnInit(): void {
  }

  public onAnswer(event: any) {
    const answer: string = event.target.value;
    const answerModel = new AnswerQuestionModel(this.question?.questionId, answer);
    this.answer.emit(answerModel);
  }
}
