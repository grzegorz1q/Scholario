import { Component, Input} from '@angular/core';
import { Student } from '../../../Type/Student';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-parent-legend',
  imports: [NgFor],
  templateUrl: './parent-legend.component.html',
  styleUrl: './parent-legend.component.scss'
})
export class ParentLegendComponent {
  @Input() children?: Student[];
  @Input() childColors: Map<number, string> = new Map();

  getChildColor(id: number): string | undefined{
    return this.childColors.get(id);
  }
}
