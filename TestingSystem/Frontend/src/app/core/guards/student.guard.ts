import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class StudentGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {
  }

  public async canActivate(): Promise<boolean> {
    if (!this.authService.loggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    if (await this.authService.isStudent()) {
      return true;
    }

    this.authService.logout();
    return false;
  }
}
