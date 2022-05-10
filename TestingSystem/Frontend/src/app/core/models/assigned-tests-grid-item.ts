export class AssignedTestsGridItem {
  constructor(
    public testId?: string,
    public testName?: string,
    public teacherName?: string,
    public status?: string,
    public questionsCount?: number,
    public results?: number
  ) {
  }
}
