import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';
import { DashboardCounts } from '../DTOs/dashboard-counts.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getDashboardCounts(tenantId: string): Observable<DashboardCounts> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'TenantId': tenantId
    });
  
    return this.http.get<DashboardCounts>(`${this.baseUrl}/api/Dashboard/counts`, { headers });
  }
  

}
