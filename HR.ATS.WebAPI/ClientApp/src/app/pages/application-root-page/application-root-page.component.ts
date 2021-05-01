import { Component, OnInit } from '@angular/core';
import { PoMenuItem } from '@po-ui/ng-components';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-application-root-page',
  templateUrl: './application-root-page.component.html',
  styleUrls: ['./application-root-page.component.css']
})
export class ApplicationRootPageComponent implements OnInit {

  title = 'ats';
  token = '';

  constructor(private oauthService: OAuthService) {}

  readonly menus: Array<PoMenuItem> = [
    { label: 'Home', action: this.onClick.bind(this) },
  ];

  private onClick(): void {
    alert('Clicked in menu item');
  }

  async ngOnInit(): Promise<void> {
    this.token = this.oauthService.authorizationHeader();
  }

  logout(): void {
    this.oauthService.logOut();
  }

}
