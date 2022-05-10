import {Component, Input, OnInit} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";

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

  constructor() { }

  ngOnInit(): void {
  }

}
