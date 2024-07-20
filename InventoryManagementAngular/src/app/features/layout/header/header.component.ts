import { CommonModule } from '@angular/common';
import { Component, ElementRef } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faBell, faGear, faPowerOff } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../auth/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule,FontAwesomeModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  userName: string | null = null;
  tenantId: string | null = null;
  tenantName: string | null = null;
  role: string | null = null;


  imagePath:string;
  constructor(private authService: AuthService, private router:Router) { 
    this.imagePath = 'assets/images/profile1.jpg';

  }



  ngOnInit(): void {
    this.userName = this.authService.getUserName();
    this.tenantId = this.authService.getTenantId();
    this.tenantName = this.authService.getTenantName();
    this.role = this.authService.getRole();
  }



  faBell=faBell;
  faLogout=faPowerOff;

  faProfile=faGear;

  isOpen: boolean = false; // Flag to track toggle state


  isDropdownOpen = false;

  toggleOpen(): void {
    this.isOpen = true;
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  logout() {
    // Clear user data and navigate to login or home page
    localStorage.removeItem('token');  // Example of clearing token
    this.router.navigate(['/login']);  // Navigate to login page
  }


  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  isManager(): boolean {
    return this.authService.isManager();
  }

 
}
