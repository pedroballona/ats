import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import jwtDecode from 'jwt-decode';
import { environment } from '../../environments/environment';
import { State } from './state';

export const authConfig: AuthConfig = {
  // Url of the Identity Provider
  issuer: environment.authorityEndpoint,

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin,

  // The SPA's id. The SPA is registered with this id at the auth-server
  clientId: environment.clientId,

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile email authorization_api offline_access',

  responseType: 'code id_token token',
};

export interface User {
  readonly name: string;
  readonly roles: Role[];
}

export enum Role {
  Candidate = 'ATS_CANDIDATE',
  Recruiter = 'ATS_RECRUITER',
}

@Injectable({
  providedIn: 'root',
})
export class AuthService extends State<User> {
  constructor(private oauthService: OAuthService) {
    super();
  }

  init(): void {
    this.oauthService.configure(authConfig);
  }

  async login(): Promise<void> {
    await this.oauthService.loadDiscoveryDocumentAndLogin({
      state: window.location.href,
      onTokenReceived: () => {
        window.location.href = this.oauthService.state as any;
      },
    });
    const obt: {
      name: string[];
      'http://www.tnf.com/identity/claims/productRole': string | string[];
    } = jwtDecode(this.oauthService.getAccessToken());
    const permissions = obt['http://www.tnf.com/identity/claims/productRole'];
    const user: User = {
      name: obt.name[1],
      roles: Array.isArray(permissions)
        ? (permissions as Role[])
        : [permissions as Role],
    };
    this.forceSetState(user);
  }

  logout(): void {
    this.oauthService.logOut();
  }

  isLogged(): boolean {
    return this.oauthService.hasValidAccessToken();
  }

  canLoggedUserAccess(menuRoles: Role[]): boolean {
    const user = this.snapshot;
    if (user == null) {
      return false;
    }
    const { roles } = user;
    return menuRoles.some((r) => !!roles.find((role) => role === r));
  }
}
