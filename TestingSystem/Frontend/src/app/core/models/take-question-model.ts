export class TakeQuestionModel {
  constructor(
    public questionId?: string,
    public questionName?: string,
    public questionType?: string,
    public selectableQuestionNames?: string[]
  ) {
  }
}
