import { DayOfWeek } from "./DayOfWeek";

export type ScheduleEntry = {
    subjectId : number;
    subjectName: string; // maybe change to optional - "?"
    groupId: number;
    groupName?: string;
    teacherName?: string;
    day: DayOfWeek;
    lessonNumber: number;
    studentId?: number;
    studentName?: string;
  }