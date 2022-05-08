export class CreateQuestionModel {
  constructor(
    public questionName?: string,
    public expectedAnswer?: string,
    public questionType?: string,
    public selectableAnswers: string[] = []
  ) {
  }
}
