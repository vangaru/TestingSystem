import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../../../core/services/account.service";
import {QuestionTypes} from "../../../../core/models/question-types";
import {FormGroupService} from "../../../../core/services/form-group.service";
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";
import * as Forms from "../../../forms/create-test-forms";
import {CreateTestModel} from "../../../../core/models/create-test-model";
import {CreateQuestionModel} from "../../../../core/models/create-question-model";
import {TestsService} from "../../../../core/services/tests.service";
import {CreateSelectableAnswerModel} from "../../../../core/models/create-selectable-answer-model";

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  private stringQuestionsArrayName = "stringQuestions";
  private checkboxQuestionsArrayName = "checkboxQuestions";
  private radiobuttonQuestionsArrayName = "radiobuttonQuestions";
  private submitted: boolean = false;

  public errorMessage: string = "";
  public studentNames: string[] = [];
  public checkBoxQuestionType = QuestionTypes.CheckboxQuestion;
  public radioButtonQuestionType = QuestionTypes.RadiobuttonQuestion;
  public stringQuestionType = QuestionTypes.StringQuestion;
  public createTestFormGroup: FormGroup = new FormGroup({});

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private testsService: TestsService,
    public formGroupService: FormGroupService) { }

  ngOnInit(): void {
    this.refreshStudentNames();
    this.createTestFormGroup = Forms.createTestFormGroup(this.formBuilder);
  }

  private refreshStudentNames() {
    this.accountService.getStudentNames().subscribe(
      studentNames => {
        this.studentNames = [...studentNames];
      }
    );
  }

  public addRadiobuttonQuestion() {
    const question: FormGroup = Forms.createRadiobuttonQuestionFormGroup(this.formBuilder);
    const questions: FormArray = this.formGroupService.getFormArray(this.createTestFormGroup, this.radiobuttonQuestionsArrayName);
    questions.push(question);
  }

  public addCheckboxQuestion() {
    const question = Forms.createCheckboxQuestionFormGroup(this.formBuilder);
    const questions: FormArray = this.formGroupService.getFormArray(this.createTestFormGroup, this.checkboxQuestionsArrayName);
    questions.push(question);
  }

  public addStringQuestion() {
    const question: FormGroup = Forms.createStringQuestionFormGroup(this.formBuilder);
    const questions: FormArray = this.formGroupService.getFormArray(this.createTestFormGroup, this.stringQuestionsArrayName);
    questions.push(question);
  }

  public deleteQuestion(questionsArrayPath: string, questionIndex: number) {
    const questions: FormArray = this.formGroupService.getFormArray(this.createTestFormGroup, questionsArrayPath);
    questions.removeAt(questionIndex);
  }

  public submitTest() {
    this.submitted = true;
    if (this.createTestFormGroup.status === "INVALID") {
      this.errorMessage = "Some required fields are not specified";
      return;
    }
    const createTestModel: CreateTestModel = this.prepareCreateTestModel();
    this.testsService.createTest(createTestModel).subscribe(
      () => {},
      () => {
        this.errorMessage = "Failed to create test due to internal server error."
      }
    );
  }

  private prepareCreateTestModel(): CreateTestModel {
    let model = new CreateTestModel();

    const formValue = this.createTestFormGroup.value;
    model.testName = formValue.testName;
    model.assignedStudentNames = [...(formValue.assignedStudentNames as Array<any>)];

    this.prepareStringQuestions(model, [...(formValue.stringQuestions as Array<any>)]);
    this.prepareRadiobuttonQuestions(model, [...formValue.radiobuttonQuestions as Array<any>]);
    this.prepareCheckboxQuestions(model, [...formValue.checkboxQuestions as Array<any>]);

    return model;
  }

  private  prepareStringQuestions(model: CreateTestModel, questions: Array<any>) {
    for (let question of questions) {
      let stringQuestion = new CreateQuestionModel();
      stringQuestion.questionName = question.questionName;
      stringQuestion.expectedAnswer = question.expectedAnswer;
      stringQuestion.questionType = QuestionTypes.StringQuestion;
      model.questions?.push(stringQuestion);
    }
  }

  private prepareRadiobuttonQuestions(model: CreateTestModel, questions: Array<any>) {
    for (let question of questions) {
      let radiobuttonQuestion = new CreateQuestionModel();
      radiobuttonQuestion.questionName = question.questionName;
      radiobuttonQuestion.expectedAnswer = question.expectedAnswer;
      radiobuttonQuestion.questionType = QuestionTypes.RadiobuttonQuestion;
      let selectableAnswers = question.selectableAnswers as Array<any>;
      selectableAnswers.forEach((answer, index) => {
        radiobuttonQuestion.selectableAnswers.push(new CreateSelectableAnswerModel(answer, index));
      })
      model.questions?.push(radiobuttonQuestion);
    }
  }

  // I know that it is duplicated
  private prepareCheckboxQuestions(model: CreateTestModel, questions: Array<any>) {
    for (let question of questions) {
      let checkboxQuestion = new CreateQuestionModel();
      checkboxQuestion.questionName = question.questionName;
      checkboxQuestion.expectedAnswer = question.expectedAnswer.toString();
      checkboxQuestion.questionType = QuestionTypes.CheckboxQuestion;
      let selectableAnswers = question.selectableAnswers as Array<any>;
      selectableAnswers.forEach((answer, index) => {
        checkboxQuestion.selectableAnswers.push(new CreateSelectableAnswerModel(answer, index));
      })
      model.questions?.push(checkboxQuestion);
    }
  }
}
