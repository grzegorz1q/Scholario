import { Component } from '@angular/core';
import { AuthService } from '../../../Service/authService';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  isExpanded = false;
  userRole: string = '';  // Zmienna do przechowywania roli użytkownika
  menuItems: any[] = [];  // Tablica do przechowywania pozycji menu

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.userRole = this.authService.getUserRole() || '';
    this.setMenuItems();
  }

  toggleSidebar(state: boolean) {
    this.isExpanded = state;
  }
  logout(){
    localStorage.removeItem("auth_token");
  }
  setMenuItems() {
    switch (this.userRole) {
      case 'Admin':
        this.menuItems = [
          { name: 'Panel Admina', link: '#' },
          { name: 'Zarządzanie użytkownikami', link: '#' },
          { name: 'Raporty', link: '#' }
        ];
        break;
      case 'Teacher':
        this.menuItems = [
          { name: 'Strona główna', link: '/schedule' },
          { name: 'Przedmioty', link: '#' },
        ];
        break;
      case 'Student':
        this.menuItems = [
          { name: 'Moje przedmioty', link: '/schedule' },
          { name: 'Oceny', link: '/grade' },
        ];
        break;
      case 'Parent':
        this.menuItems = [
          { name: 'Plan zajęć dziecka', link: '/schedule' },
          { name: 'Komunikacja z nauczycielami', link: '#' }
        ];
        break;
      default:
        this.menuItems = [];
        break;
    }
  }
}