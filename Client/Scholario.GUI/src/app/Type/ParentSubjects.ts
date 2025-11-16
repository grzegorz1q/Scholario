import { Subject } from "./Subject";

export interface ParentSubjects{
    childId: number;
    childName: string;
    subjects: Subject[];
}