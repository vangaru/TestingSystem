import {FormBuilder, FormGroup, Validators} from "@angular/forms";

export function createTestFormGroup(formBuilder: FormBuilder): FormGroup {
  return formBuilder.group({
    testName: formBuilder.control(null, Validators.required),
    assignedStudentNames: formBuilder.control(null, Validators.required),
    stringQuestions: formBuilder.array([], Validators.required),
    radiobuttonQuestions: formBuilder.array([], Validators.required),
    checkboxQuestions: formBuilder.array([], Validators.required)
  });
}

export function createStringQuestionFormGroup(formBuilder: FormBuilder): FormGroup {
  return formBuilder.group({
    questionName: formBuilder.control(null, Validators.required),
    expectedAnswer: formBuilder.control(null, Validators.required)
  });
}

export function createRadiobuttonQuestionFormGroup(formBuilder: FormBuilder): FormGroup {
  return formBuilder.group({
    questionName: formBuilder.control(null, Validators.required),
    expectedAnswer: formBuilder.control([], Validators.required),
    selectableAnswers: formBuilder.array([], Validators.required)
  });
}

export function createCheckboxQuestionFormGroup(formBuilder: FormBuilder): FormGroup {
  return formBuilder.group({
    questionName: formBuilder.control(null, Validators.required),
    expectedAnswer: formBuilder.control([], Validators.required),
    selectableAnswers: formBuilder.array([], Validators.required)
  })
}
