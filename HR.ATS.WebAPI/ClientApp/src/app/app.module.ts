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
import { OpeningAppliedApplicantsListingPageComponent } from './pages/opening-applied-applicants-listing-page/opening-applied-applicants-listing-page.component';
import { OpeningListingPageComponent } from './pages/opening-listing-page/opening-listing-page.component';
import { OpeningFormComponent } from './pages/opening-management-page/components/opening-form/opening-form.component';
import { OpeningManagementPageComponent } from './pages/opening-management-page/opening-management-page.component';
import { PreLoaderPageComponent } from './pages/pre-loader-page/pre-loader-page.component';
import { ResumeFormComponent } from './pages/resume-page/components/resume-form/resume-form.component';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';
import { ResumeForRecruiterPageComponent } from './pages/resume-for-recruiter-page/resume-for-recruiter-page.component';


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
    OpeningListingPageComponent,
    OpeningAppliedApplicantsListingPageComponent,
    ResumeForRecruiterPageComponent
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
