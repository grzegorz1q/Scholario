import { ParentSubjects } from "./ParentSubjects";
import { Subject } from "./Subject";

export interface LoggedUserSubjects{
    subjects?: Subject[];
    parentSubjects?: ParentSubjects[];
}