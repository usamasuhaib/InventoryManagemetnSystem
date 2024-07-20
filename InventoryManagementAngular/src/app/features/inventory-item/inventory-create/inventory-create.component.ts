import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../auth/auth.service';
import { InventoryService } from '../../../services/inventory.service';
import { InventoryItemDto } from '../../../DTOs/inventory-item-dto.model';
import { WarehouseService } from '../../../services/warehouse.service';
import { Warehouse } from '../../../models/warehouse.model';

@Component({
  selector: 'app-inventory-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './inventory-create.component.html',
  styleUrl: './inventory-create.component.css'
})
export class InventoryCreateComponent {

  tenantId: string | null = '';
  InventoryForm!: FormGroup;

  warehouses: Warehouse[] = [];
  selectedWarehouseId: number | undefined;


  constructor(private authService: AuthService, private warehouseService: WarehouseService, private inventoryService: InventoryService, private route: Router, private formBuilder: FormBuilder, private title: Title, private toaster:ToastrService) {
    this.InventoryForm = this.formBuilder.group({
      name: ['',Validators.required],
      description: ['',Validators.required],
      price: [0,Validators.required],
      quantity: [0,Validators.required],
      category: ['',Validators.required],
    
    });

  }

  ngOnInit(): void {
    this.title.setTitle(`Add Inventory`)
    this.tenantId = this.authService.getTenantId();

    if(this.tenantId){
      this.loadWarehouses(this.tenantId);
    }
    else{
      console.log("tenant id is missing");
    }

    console.log(this.warehouses);
   
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

  onWarehouseChange(event: any): void {
    this.selectedWarehouseId = +event.target.value;
  }

  createInventoryItem():void{
    console.log(this.InventoryForm.value);
    if(this.InventoryForm.valid){
      const data:InventoryItemDto={
        name:this.InventoryForm.value.name!,
        description:this.InventoryForm.value.description!,
        price: this.InventoryForm.value.price!,
        quantity: this.InventoryForm.value.quantity!,
        category:this.InventoryForm.value.category!,
        wareHouseId:this.selectedWarehouseId!,
      }

      console.log(data);

      // this.tenantId = this.authService.getTenantId();
      if (this.tenantId) {
        this.inventoryService.createInventoryItem(data, this.tenantId).subscribe(
          (createdItem) => {
            console.log('Inventory item created successfully', createdItem);
            this.route.navigate(['/inventory/list']);
            this.toaster.success("New Inventoty Created Sussfully");
          },
          (error) => {
            console.error('Error creating inventory item', error);
          }
        );
      } else {
        console.error('Tenant ID is missing');
      }

    }

    else{
      alert("invalid data, try again please")
    }
  }





}
