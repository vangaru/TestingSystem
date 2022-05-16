import {CreateSelectableAnswerModel} from "./create-selectable-answer-model";

export class CreateQuestionModel {
  constructor(
    public questionName?: string,
    public expectedAnswer?: string,
    public questionType?: string,
    public selectableAnswers: CreateSelectableAnswerModel[] = []
  ) {
  }
}
