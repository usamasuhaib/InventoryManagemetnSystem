import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faEdit, faEye, faRemove, faTrash, faWarning } from '@fortawesome/free-solid-svg-icons';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { InventoryService } from '../../../services/inventory.service';
import { InventoryItem } from '../../../models/inventory-item.model';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-inventory-list',
  standalone: true,
  imports: [FontAwesomeModule, CommonModule, RouterLink],
  templateUrl: './inventory-list.component.html',
  styleUrl: './inventory-list.component.css'
})
export class InventoryListComponent {
  faEdit=faEdit;
  faDelete=faTrash;
  faView=faEye;
  faWarning=faWarning;

  inventoryItems: InventoryItem[] = [];
  errorMessage: string = '';
  tenantId: string | null = null;

  constructor(
    private httpClient: HttpClient,
    private toaster: ToastrService,
    private title:Title,
    private router:Router,
    private inventoryService:InventoryService,
    private authService:AuthService
  
  ) {

  }


  ngOnInit(): void {
    // this.tenantId = 'tenant2'; 
    this.tenantId = this.authService.getTenantId();

    if (this.tenantId) {
      this.inventoryService.getInventoryItems(this.tenantId).subscribe(
        (items) => {
          this.inventoryItems = items;
        },
        (error) => {
          console.error('Error fetching inventory items', error);
        }
      );
    } else {
      console.error('Tenant ID is missing');
    }
  }


  onEdit(id: any) {
    this.router.navigate(['inventory/update', id]);

  }

deleteInventoryItem(id: any): void {
  this.tenantId = this.authService.getTenantId();
  
  if (this.tenantId) {
    this.inventoryService.deleteInventoryItem(id, this.tenantId).subscribe(
      () => {
        console.log('Inventory item deleted successfully');
        this.router.navigate(['/inventory/list']);
        this.toaster.success("Item deleted successfully")
      },
      (error) => {
        console.error('Error deleting inventory item', error);
      }
    );
  } else {
    console.error('Tenant ID is missing');
  }
}


  addInventory(){
    return this.router.navigate(['admin/add-std']);
  }


  onStudentDetails(id:any){
    return this.router.navigate(['admin/std-details/'+id]);
  }




}
