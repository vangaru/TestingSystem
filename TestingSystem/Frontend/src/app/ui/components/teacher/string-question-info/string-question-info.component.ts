import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AbstractControl, FormControl, FormGroup} from "@angular/forms";
import {FormGroupService} from "../../../../core/services/form-group.service";

@Component({
  selector: 'app-string-question-info',
  templateUrl: './string-question-info.component.html',
  styleUrls: ['./string-question-info.component.css']
})
export class StringQuestionInfoComponent implements OnInit {

  public expectedAnswer?: string;
  @Output()
  public onCorrectAnswerSelected: EventEmitter<string> = new EventEmitter<string>();

  @Input()
  public correctAnswerFormControl: FormControl = new FormControl();

  constructor(public formGroupService: FormGroupService) { }

  ngOnInit(): void {
  }

  public onAnswerSelected() {
    this.onCorrectAnswerSelected.emit(this.expectedAnswer);
  }
}
