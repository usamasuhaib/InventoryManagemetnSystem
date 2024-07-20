import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Warehouse } from '../../../models/warehouse.model';
import { CommonModule } from '@angular/common';
import { InventoryService } from '../../../services/inventory.service';
import { AuthService } from '../../../auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { WarehouseService } from '../../../services/warehouse.service';
import { InventoryItemDto } from '../../../DTOs/inventory-item-dto.model';

@Component({
  selector: 'app-inventory-update',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './inventory-update.component.html',
  styleUrl: './inventory-update.component.css'
})
export class InventoryUpdateComponent {
  InventoryForm: FormGroup;
  warehouses: Warehouse[] = [];
  tenantId: string | null = '';
  inventoryId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private warehouseService: WarehouseService,
  ) {
    this.InventoryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      quantity: ['', Validators.required],
      category: ['', Validators.required],
      wareHouseId: ['', Validators.required]
    });

    // this.itemId = this.route.snapshot.paramMap.get('id') || '';
  }

  ngOnInit(): void {

    this.tenantId = this.authService.getTenantId();

    if (this.tenantId) {
      this.loadWarehouses(this.tenantId);
    }
    else {
      console.log("tenant id is missing");
    }

    this.route.paramMap.subscribe(params => {
      this.inventoryId = params.get('id');
      if (this.inventoryId) {
        this.loadInventoryItem(this.inventoryId);
      }
    });






  }
  loadWarehouses(tenantId: string): void {
    this.warehouseService.getWarehouses(tenantId).subscribe(
      warehouses => {
        this.warehouses = warehouses;
      },
      error => {
        console.error('Error fetching warehouses', error);
      }
    );
  }

  loadInventoryItem(id: string): void {
    if (this.tenantId) {
      this.inventoryService.getInventoryItem(id, this.tenantId).subscribe(
        (item) => {
          this.InventoryForm.patchValue(item);
          console.log(item);
        },
        (error) => {
          console.error('Error loading inventory item', error);
        }
      );
    } else {
      console.error('Tenant ID is missing');
    }
  }


  onWarehouseChange(event: any): void {
    this.InventoryForm.patchValue({ wareHouseId: event.target.value });
  }

  // inventory.component.ts
  updateInventoryItem(): void {
  this.tenantId = this.authService.getTenantId();
  
  if (this.tenantId) {
    const updatedData: InventoryItemDto = this.InventoryForm.value;
    
    this.inventoryService.updateInventoryItem(this.inventoryId!, updatedData, this.tenantId).subscribe(
      (updatedItem) => {
        console.log('Inventory item updated successfully', updatedItem);

        this.router.navigate(['/inventory/list']);
      },
      (error) => {
        console.error('Error updating inventory item', error);
      }
    );
  } else {
    console.error('Tenant ID is missing');
  }
}



  backToList(): void {
    this.router.navigate(['/inventory/list']);
  }
}
