import { ScheduleEntry } from "./ScheduleEntry"

export type Classroom = {
    id: number,
    number: number,
    capacity: number,
    scheduleEntries: ScheduleEntry[],
    occupied?: boolean
}

