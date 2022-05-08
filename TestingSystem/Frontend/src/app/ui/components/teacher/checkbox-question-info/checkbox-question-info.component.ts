import {Component, Input, OnInit} from '@angular/core';
import {FormArray, FormControl} from "@angular/forms";
import {FormGroupService} from "../../../../core/services/form-group.service";
import {v4} from "uuid";

@Component({
  selector: 'app-checkbox-question-info',
  templateUrl: './checkbox-question-info.component.html',
  styleUrls: ['./checkbox-question-info.component.css']
})
export class CheckboxQuestionInfoComponent implements OnInit {

  @Input()
  public expectedAnswerFormControl: FormControl = new FormControl();

  @Input()
  public selectableAnswersFormArray: FormArray = new FormArray([]);

  public uuid: string = v4();

  constructor(public formGroupService: FormGroupService) { }

  ngOnInit(): void {
  }

  public addAnswer() {
    this.selectableAnswersFormArray.push(new FormControl());
  }

  public deleteAnswer(answerId: number) {
    this.selectableAnswersFormArray.removeAt(answerId);
  }
}
