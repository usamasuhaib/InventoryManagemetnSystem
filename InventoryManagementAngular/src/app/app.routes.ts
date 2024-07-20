import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { DashboardComponent } from './features/layout/dashboard/dashboard.component';
import { authGuard } from './auth/auth.guard';
import { loginGuard } from './guards/login.guard';

export const routes: Routes = [
    { path: 'login', component: LoginComponent, canActivate: [loginGuard] },
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
    // Add other protected routes as needed
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },

    { path: 'inventory', loadChildren: () => import('./features/inventory-item/inventory-item.module').then(m => m.InventoryItemModule), canActivate: [authGuard] },


    // //invebtory module route
    // {
    //     path: 'inventory',
    //     loadChildren: () => import('./features/inventory-item/inventory-item.module').then(m => m.InventoryItemModule),
    //     canActivate: [authGuard],

    // },

    //warehouse module route
    {
        path: 'warehouse',
        loadChildren: () => import('./features/warehouse/warehouse.module').then(m => m.WarehouseModule),
        canActivate: [authGuard],

    },
];
