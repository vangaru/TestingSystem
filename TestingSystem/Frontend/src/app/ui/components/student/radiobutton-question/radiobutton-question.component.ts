import {Component, Input, OnInit} from '@angular/core';
import {TakeQuestionModel} from "../../../../core/models/take-question-model";
import {v4} from "uuid";

@Component({
  selector: 'app-radiobutton-question',
  templateUrl: './radiobutton-question.component.html',
  styleUrls: ['./radiobutton-question.component.css']
})
export class RadiobuttonQuestionComponent implements OnInit {

  public questionAnswer: string = "";

  @Input()
  public question?: TakeQuestionModel;

  public uuid: string = v4();

  constructor() { }

  ngOnInit(): void {
  }

}
