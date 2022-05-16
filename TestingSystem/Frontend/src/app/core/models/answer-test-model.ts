import {AnswerQuestionModel} from "./answer-question-model";

export class AnswerTestModel {
  constructor(
    public testId?: string,
    public questionAnswers: AnswerQuestionModel[] = []
  ) {
  }
}
