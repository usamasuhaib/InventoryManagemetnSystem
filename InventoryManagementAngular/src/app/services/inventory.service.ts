import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InventoryItem } from '../models/inventory-item.model';
import { Observable } from 'rxjs';
import { InventoryItemDto } from '../DTOs/inventory-item-dto.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInventoryItems(tenantId: string): Observable<InventoryItem[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'TenantId': tenantId // Include the TenantId header
    });

    return this.http.get<InventoryItem[]>(`${this.baseUrl}/api/Inventory/GetInventoryItems`, { headers });
  }


  createInventoryItem(data: InventoryItemDto, tenantId: string): Observable<any> {
    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'TenantId': tenantId
    });

    return this.http.post<any>(`${this.baseUrl}/api/Inventory/CreateInventoryItem`, data, { headers });
  }

  getInventoryItem(id: string, tenantId: string | null): Observable<InventoryItemDto> {
    if (!tenantId) {
      throw new Error('Tenant ID is required');
    }

    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('TenantId', tenantId);

    return this.http.get<InventoryItemDto>(`${this.baseUrl}/api/inventory/GetInventoryItemById/${id}`, { headers });
  }


  updateInventoryItem(id: string, data: InventoryItemDto, tenantId: string): Observable<InventoryItemDto> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`)
      .set('TenantId', tenantId);
  
    return this.http.put<InventoryItemDto>(`${this.baseUrl}/api/Inventory/UpdateInventory/${id}`, data, { headers });
  }


  deleteInventoryItem(id: string, tenantId: string): Observable<void> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`).set('TenantId', tenantId);
    return this.http.delete<void>(`${this.baseUrl}/api/inventory/DeleteInventoryItem/${id}`, { headers });
  }

}
