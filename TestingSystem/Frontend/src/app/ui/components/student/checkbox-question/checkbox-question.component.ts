import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";
import {AnswerQuestionModel} from "../../../../core/models/answer-question-model";

@Component({
  selector: 'app-checkbox-question',
  templateUrl: './checkbox-question.component.html',
  styleUrls: ['./checkbox-question.component.css']
})
export class CheckboxQuestionComponent implements OnInit {

  public questionAnswer: string = "";
  public uuid: string = v4();

  @Input()
  public question?: TakeQuestionModel;

  @Output()
  public answer: EventEmitter<AnswerQuestionModel> = new EventEmitter<AnswerQuestionModel>();

  constructor() { }

  ngOnInit(): void {
  }

  public onAnswer(event: any) {
    const answer: string = event.checked.toString();
    const answerModel: AnswerQuestionModel = new AnswerQuestionModel(this.question?.questionId, answer);
    this.answer.emit(answerModel);
  }
}
