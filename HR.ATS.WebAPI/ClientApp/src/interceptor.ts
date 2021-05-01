import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs';

@Injectable()
export class AtsInterceptor implements HttpInterceptor {
  constructor(private oauthService: OAuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const header = this.oauthService.authorizationHeader();
    req = req.clone({
      setHeaders: {
        Authorization: header
      }
    });
    return next.handle(req);
  }

}
