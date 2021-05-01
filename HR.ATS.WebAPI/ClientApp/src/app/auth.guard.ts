import { Injectable } from '@angular/core';
import {
  CanActivate
} from '@angular/router';
import { AuthService } from './utils/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService) {}

  async canActivate(): Promise<boolean> {
    await this.authService.login();
    const isLogged = this.authService.isLogged();
    return isLogged;
  }
}
