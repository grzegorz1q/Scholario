import { Group } from "./Group";

export type Subject = {
    id: number
    name: string
    desctription: string
    teacherName: string
    groups: Group[]
  };
  