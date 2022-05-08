import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-string-question-info',
  templateUrl: './string-question-info.component.html',
  styleUrls: ['./string-question-info.component.css']
})
export class StringQuestionInfoComponent implements OnInit {

  public expectedAnswer?: string;
  @Output()
  public onCorrectAnswerSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  public onAnswerSelected() {
    this.onCorrectAnswerSelected.emit(this.expectedAnswer);
  }
}
