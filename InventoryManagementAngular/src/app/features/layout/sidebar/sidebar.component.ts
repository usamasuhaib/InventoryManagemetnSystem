import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faAd, faAdd, faCaretDown, faChalkboardTeacher, faChevronCircleRight, faChevronDown, faChevronRight, faChevronUp, faClipboardList, faCog, faFileInvoiceDollar, faHome, faList, faPlus, faPowerOff, faSwatchbook, faUser, faUserGraduate, faWarehouse } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../../auth/auth.service';


@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule,FontAwesomeModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {



  imagePath: string;
  faDashboard=faHome;
  faUser=faUser;
  faAdd=faAdd
  faWarehouse=faWarehouse
  faInventory=faSwatchbook


  faChevronUp = faChevronUp;
  faChevronRight=faChevronCircleRight
  faChevronDown = faChevronDown;



  navDialog: HTMLElement | undefined;

  constructor(private route:Router, private authService:AuthService) {
    this.imagePath = 'assets/images/erplogo.png';

    

   }

   ngOnInit(): void {
 
  this.isAdmin();
  this.isManager();
   }




   


 


   isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  isManager(): boolean {
    return this.authService.isManager();
  }

  isInventoryMenuOpen = false;
  isWareHouseMenuOpen = false;
  isUserMenuOpen = false;


  toggleMenu(menu: string) {
    switch (menu) {
      case 'user':
        this.isUserMenuOpen = !this.isUserMenuOpen;
        break;
      case 'inventory':
        this.isInventoryMenuOpen = !this.isInventoryMenuOpen;
        break;
      case 'warehouse':
        this.isWareHouseMenuOpen = !this.isWareHouseMenuOpen;
        break;
    }
  }

 

 
  AddStd(){
    return this.route.navigate(['admin/add-std']);
  }
  AddTeacher(){
    return this.route.navigate(['admin/add-teacher']);

  }
  
  stdReports() {
    // Navigate to student reports or handle the click event
  }

  handleMenu(){
    this.navDialog?.classList.toggle('hidden')
  }

  InventoryList(){

    return this.route.navigate(['/inventory/list'])

  }
  dashboard(){
    return this.route.navigate(['/dashboard']);
  }
  CreateInventory(){
    return this.route.navigate(['/inventory/create'])

  }


  logOut(){
    localStorage.removeItem('token')
    return this.route.navigate(['login']);
  }
}
