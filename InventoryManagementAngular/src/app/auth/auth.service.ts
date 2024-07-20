import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../environments/environment.prod';
import { DecodedToken } from '../models/decoded-token.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenKey = 'token';
  private decodedToken: DecodedToken | null = null;
  private baseUrl = environment.apiUrl;

 


  constructor(private httpClient: HttpClient,private router: Router ,private toaster:ToastrService) {
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      this.decodeToken(token);
    }
  }


  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

 

  onLogin(obj: any): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}/api/Auth/login`, obj);
  }

  storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this.decodeToken(token);
  }


  private decodeToken(token: string): void {
    this.decodedToken = jwtDecode<DecodedToken>(token);
  }



isLoggedIn(): boolean {
    const token = localStorage.getItem(this.tokenKey);
    return !!token;
  }

  getUserName(): string | null {
    return this.decodedToken ? this.decodedToken.UserName : null;
  }

  getTenantId(): string | null {
    return this.decodedToken ? this.decodedToken.TenantId : null;
  }

  getTenantName(): string | null {
    return this.decodedToken ? this.decodedToken.TenantName : null;
  }




  
    logout(){
      localStorage.removeItem('token');

      this.toaster.success("Logged Out successfully")
      this.router.navigate(['login']);
    }

    removeToken(){
      localStorage.removeItem('token');

    }



  getRole(): string | null {
    return this.decodedToken ? this.decodedToken.role : null;
  }
  isAdmin(): boolean {
    const role = this.getRole();
    return role === 'Admin';
  }
  
  isManager(): boolean {
    const role = this.getRole();
    return role === 'Manager';
  }


}
