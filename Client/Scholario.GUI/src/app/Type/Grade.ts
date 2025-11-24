
export type Grade = {
  id?: number;
  gradeValue: number;
  gradeWeight: number;
  gradeWeightName?: string;
  subjectName?: string;
  subjectId: number,
  studentId: number;
  dateOfIssue?: Date;
  descriptiveAssessmentId?: number;
};