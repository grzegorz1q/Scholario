
export type Grade = {
  gradeValue: number;
  gradeWeight: number;
  gradeWeightName?: string;
  subjectName: string;
  subjectId: number,
  studentId: number;
  dateOfIssue?: Date;
  descriptiveAssessmentId: number;
};