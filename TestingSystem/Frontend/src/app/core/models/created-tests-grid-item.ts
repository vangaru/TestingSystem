export class CreatedTestsGridItem {
  constructor(
    public testId?: string,
    public testName?: string,
    public questionsCount?: number,
    public resultsCount?: number,
    public assignedStudentsCount?: number
  ) {
  }
}
