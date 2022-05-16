import { Component, OnInit } from '@angular/core';
import {TakeTestModel} from "../../../../core/models/take-test-model";
import {ActivatedRoute} from "@angular/router";
import {TestsService} from "../../../../core/services/tests.service";
import {AnswerQuestionModel} from "../../../../core/models/answer-question-model";
import {AnswerTestModel} from "../../../../core/models/answer-test-model";

@Component({
  selector: 'app-take-test',
  templateUrl: './take-test.component.html',
  styleUrls: ['./take-test.component.css']
})
export class TakeTestComponent implements OnInit {

  private answers: AnswerTestModel = new AnswerTestModel();

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

  public onAnswer(event: AnswerQuestionModel) {
    const answer: AnswerQuestionModel = event;
    this.answers.questionAnswers.forEach(qa => {
      if (qa.questionId === answer.questionId) {
        this.answers.questionAnswers = [...this.answers.questionAnswers.filter(a => a.questionId !== answer.questionId)]
      }
    })
    this.answers.questionAnswers?.push(event);
  };

  public onSubmit() {
    this.answers.testId = this.test?.testId;
    this.testsService.answerTest(this.answers).subscribe();
  }
}
