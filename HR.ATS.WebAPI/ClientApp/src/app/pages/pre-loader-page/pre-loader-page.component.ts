import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-pre-loader-page',
  templateUrl: './pre-loader-page.component.html',
  styleUrls: ['./pre-loader-page.component.css']
})
export class PreLoaderPageComponent implements OnInit {

  constructor(private oauthService: OAuthService, private router: Router) { }

  ngOnInit(): void {
    this.router.navigate(['app']);
  }

}
