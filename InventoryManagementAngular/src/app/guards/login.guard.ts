import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { inject } from '@angular/core';

  export const loginGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthService);
  const router = inject(Router);



  if (authService.isLoggedIn()) {
    router.navigate(['/dashboard']);
    return false;
  }
  return true;
};
