import { Grade } from "./Grade";

export type Student = {
    id: number;
    firstName: string;
    lastName: string;
    grades: Grade[];
}