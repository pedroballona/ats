import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { PoModule } from '@po-ui/ng-components';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AtsInterceptor } from '../interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApplicationRootPageComponent } from './pages/application-root-page/application-root-page.component';
import { PreLoaderPageComponent } from './pages/pre-loader-page/pre-loader-page.component';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';


@NgModule({
  declarations: [
    AppComponent,
    ResumePageComponent,
    PreLoaderPageComponent,
    ApplicationRootPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    HttpClientModule,
    PoModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AtsInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
