import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Warehouse } from '../models/warehouse.model';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}


  getWarehouses(tenantId: string): Observable<Warehouse[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'TenantId': tenantId
    });

    return this.http.get<Warehouse[]>(`${this.baseUrl}/api/Warehouse/GetWarehouses`, { headers });
  }


}
