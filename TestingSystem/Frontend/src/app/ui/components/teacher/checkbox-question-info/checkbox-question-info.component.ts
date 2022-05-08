import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkbox-question-info',
  templateUrl: './checkbox-question-info.component.html',
  styleUrls: ['./checkbox-question-info.component.css']
})
export class CheckboxQuestionInfoComponent implements OnInit {

  public answers: string[] = [];

  constructor() { }

  ngOnInit(): void {
  }
}
