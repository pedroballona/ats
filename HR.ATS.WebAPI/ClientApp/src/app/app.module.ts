import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { PoModule } from '@po-ui/ng-components';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AtsInterceptor } from '../interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApplicationRootPageComponent } from './pages/application-root-page/application-root-page.component';
import { HomeComponent } from './pages/home/home.component';
import { PreLoaderPageComponent } from './pages/pre-loader-page/pre-loader-page.component';
import { ResumeFormComponent } from './pages/resume-page/components/resume-form/resume-form.component';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';
import { OpeningManagementPageComponent } from './pages/opening-management-page/opening-management-page.component';
import { OpeningFormComponent } from './pages/opening-management-page/components/opening-form/opening-form.component';
import { OpeningListingPageComponent } from './pages/opening-listing-page/opening-listing-page.component';


@NgModule({
  declarations: [
    AppComponent,
    ResumePageComponent,
    PreLoaderPageComponent,
    ApplicationRootPageComponent,
    HomeComponent,
    ResumeFormComponent,
    OpeningManagementPageComponent,
    OpeningFormComponent,
    OpeningListingPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    OAuthModule.forRoot(),
    HttpClientModule,
    PoModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AtsInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
