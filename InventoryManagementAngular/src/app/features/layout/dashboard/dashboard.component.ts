import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { faChalkboardTeacher, faUniversalAccess, faUserGraduate, faUserGroup } from '@fortawesome/free-solid-svg-icons';
import { InventoryService } from '../../../services/inventory.service';
import { WarehouseService } from '../../../services/warehouse.service';
import { DashboardCounts } from '../../../DTOs/dashboard-counts.model';
import { AuthService } from '../../../auth/auth.service';
import { DashboardService } from '../../../services/dashboard.service';
import { FaLayersCounterComponent, FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule,FontAwesomeModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


  
  faInventory=faUniversalAccess

  faUserFriends = faUserGroup; // Corrected icon variable

  dashboardCounts: DashboardCounts = { warehouseCount: 0, inventoryCount: 0 }; // Correctly typed single object
  tenantId: string | null = null;

  constructor(
    private inventoryService: InventoryService,
    private dashboardService: DashboardService,
    private wareHouseService: WarehouseService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.tenantId = this.authService.getTenantId();
    if (this.tenantId) {
      this.dashboardService.getDashboardCounts(this.tenantId).subscribe(
        (counts) => {
          this.dashboardCounts = counts;
        },
        (error) => {
          console.error('Error fetching dashboard counts', error);
        }
      );
    } else {
      console.error('Tenant ID is missing');
    }
  }
}
