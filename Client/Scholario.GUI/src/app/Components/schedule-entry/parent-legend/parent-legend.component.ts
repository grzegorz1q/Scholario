import { Component, Input} from '@angular/core';
import { Student } from '../../../Type/Student';

@Component({
  selector: 'app-parent-legend',
  imports: [],
  templateUrl: './parent-legend.component.html',
  styleUrl: './parent-legend.component.scss'
})
export class ParentLegendComponent {
  @Input() childs?: Student[];

}
