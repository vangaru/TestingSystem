import {TakeSelectableAnswerModel} from "./take-selectable-answer-model";

export class TakeQuestionModel {
  constructor(
    public questionId?: string,
    public questionName?: string,
    public questionType?: string,
    public selectableQuestions?: TakeSelectableAnswerModel[]
  ) {
  }
}
