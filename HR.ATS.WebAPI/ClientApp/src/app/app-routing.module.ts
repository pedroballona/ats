import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ApplicationRootPageComponent } from './pages/application-root-page/application-root-page.component';
import { HomeComponent } from './pages/home/home.component';
import { OpeningAppliedApplicantsListingPageComponent } from './pages/opening-applied-applicants-listing-page/opening-applied-applicants-listing-page.component';
import { OpeningListingPageComponent } from './pages/opening-listing-page/opening-listing-page.component';
import { OpeningManagementPageComponent } from './pages/opening-management-page/opening-management-page.component';
import { ResumeForRecruiterPageComponent } from './pages/resume-for-recruiter-page/resume-for-recruiter-page.component';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';

const routes: Routes = [
  {
    path: '',
    component: ApplicationRootPageComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: HomeComponent },
      { path: 'resume', component: ResumePageComponent },
      { path: 'opening/management', component: OpeningManagementPageComponent },
      {
        path: 'opening/management/:id/applied/applicants',
        component: OpeningAppliedApplicantsListingPageComponent,
      },
      {
        path: 'opening/management/:openingId/applied/applicants/:applicantId/resume',
        component: ResumeForRecruiterPageComponent,
      },
      { path: 'opening/listing', component: OpeningListingPageComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
