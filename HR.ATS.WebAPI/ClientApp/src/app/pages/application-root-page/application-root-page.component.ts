import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { MenuService } from '../../utils/menu.service';

@Component({
  selector: 'app-application-root-page',
  templateUrl: './application-root-page.component.html',
  styleUrls: ['./application-root-page.component.css']
})
export class ApplicationRootPageComponent implements OnInit {

  title = 'ats';
  token = '';

  constructor(private oauthService: OAuthService, private menuService: MenuService) {}

  readonly menus$ = this.menuService.menu$;

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
