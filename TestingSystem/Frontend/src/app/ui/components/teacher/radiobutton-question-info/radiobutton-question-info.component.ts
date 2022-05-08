import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormControl} from "@angular/forms";
import {FormGroupService} from "../../../../core/services/form-group.service";
import {v4} from "uuid";

@Component({
  selector: 'app-radiobutton-question-info',
  templateUrl: './radiobutton-question-info.component.html',
  styleUrls: ['./radiobutton-question-info.component.css']
})
export class RadiobuttonQuestionInfoComponent implements OnInit {

  @Input()
  public expectedAnswerFormControl: FormControl = new FormControl();

  @Input()
  public selectableAnswersFormArray: FormArray = new FormArray([]);

  public uuid: string = v4();

  constructor(public formGroupService: FormGroupService) { }

  ngOnInit(): void {
  }

  public addAnswer() {
    const answer = new FormControl();
    this.selectableAnswersFormArray.push(answer);
  }

  public deleteAnswer(answerIndex: number) {
    this.selectableAnswersFormArray.removeAt(answerIndex);
  }
}
