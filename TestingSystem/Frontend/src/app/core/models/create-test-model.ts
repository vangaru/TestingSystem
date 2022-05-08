import {CreateQuestionModel} from "./create-question-model";

export class CreateTestModel {
  constructor(
    public testName?: string,
    public assignedStudentNames?: string[],
    public questions: CreateQuestionModel[] = []
  ) {
  }
}
