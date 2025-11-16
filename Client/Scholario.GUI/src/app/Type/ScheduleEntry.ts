export type ScheduleEntry = {
    subjectId : number;
    subjectName: string; // maybe change to optional - "?"
    groupId: number;
    groupName?: string;
    teacherName?: string;
    day: number;
    lessonNumber: number;
    studentId?: number;
    studentName?: string;
  }