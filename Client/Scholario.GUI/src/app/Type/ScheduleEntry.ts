import { DayOfWeek } from "./DayOfWeek";

export type ScheduleEntry = {
    subjectId : number;
    subjectName: string; // maybe change to optional - "?"
    groupId: number;
    groupName?: string;
    teacherName?: string;
    day: DayOfWeek;
    lessonNumber: number;
    classroomNumber?: number; // must be included in creating schedule entry (timetable component) 
    studentId?: number;
    studentName?: string;
    _isNew?: boolean;
  }