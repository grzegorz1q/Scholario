import { Group } from "./Group";

export type Subject = {
    id: number
    name: string
    description: string
    teacherName: string
    groups: Group[]
  };
  