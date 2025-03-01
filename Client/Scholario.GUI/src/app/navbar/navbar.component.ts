import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isExpanded = false;  

  toggleSidebar(state: boolean): void {
    this.isExpanded = state; 
  }
}