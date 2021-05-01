import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import {
  CanActivate
} from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private oauthService: OAuthService, private location: Location) {}

  async canActivate(): Promise<boolean> {
    await this.oauthService.loadDiscoveryDocumentAndLogin({
      state: window.location.href,
      onTokenReceived: () => {
        window.location.href = this.oauthService.state as any;
      },
    });
    const isLogged = this.oauthService.hasValidAccessToken();
    return isLogged;
  }
}
