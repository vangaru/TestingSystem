import {Component, Input, OnInit} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";

@Component({
  selector: 'app-string-question',
  templateUrl: './string-question.component.html',
  styleUrls: ['./string-question.component.css']
})
export class StringQuestionComponent implements OnInit {

  @Input()
  public question?: TakeQuestionModel;

  public uuid: string = v4();

  constructor() { }

  ngOnInit(): void {
  }

}
