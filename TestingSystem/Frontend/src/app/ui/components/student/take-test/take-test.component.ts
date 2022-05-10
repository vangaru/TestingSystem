import { Component, OnInit } from '@angular/core';
import {TakeTestModel} from "../../../../core/models/take-test-model";
import {ActivatedRoute} from "@angular/router";
import {TestsService} from "../../../../core/services/tests.service";

@Component({
  selector: 'app-take-test',
  templateUrl: './take-test.component.html',
  styleUrls: ['./take-test.component.css']
})
export class TakeTestComponent implements OnInit {

  public test?: TakeTestModel;
  public stringQuestionType: string = "String";
  public checkboxQuestionType: string = "Checkbox";
  public radiobuttonQuestionType: string = "Radiobutton";

  constructor(private activatedRoute: ActivatedRoute, private testsService: TestsService) { }

  ngOnInit(): void {
    const testId = this.activatedRoute.snapshot.params["id"];
    this.testsService.getAssignedTest(testId).subscribe((test) => {
      this.test = test;
      console.log(this.test);
    });
  }

}
