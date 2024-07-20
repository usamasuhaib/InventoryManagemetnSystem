import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from "./features/layout/sidebar/sidebar.component";
import { HeaderComponent } from "./features/layout/header/header.component";
import { CommonModule } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, HeaderComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'InventoryManagementAngular';
  isLoginPage: boolean = true;

  /**
   *
   */
  constructor(public authService:AuthService) {

    
  }
}
