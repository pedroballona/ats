import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-resume-page',
  templateUrl: './resume-page.component.html',
  styleUrls: ['./resume-page.component.css']
})
export class ResumePageComponent implements OnInit {

  constructor(private oauthService: OAuthService) { }

  ngOnInit(): void {
  }

  logout(): void {
    this.oauthService.logOut();
  }

}
