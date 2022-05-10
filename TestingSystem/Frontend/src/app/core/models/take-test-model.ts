import {TakeQuestionModel} from "./take-question-model";

export class TakeTestModel {
  constructor(
    public testId?: string,
    public testName?: string,
    public questions?: TakeQuestionModel[]
  ) {
  }
}
