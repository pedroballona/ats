import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ApplicationRootPageComponent } from './pages/application-root-page/application-root-page.component';
import { HomeComponent } from './pages/home/home.component';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';

const routes: Routes = [
  {
    path: '',
    component: ApplicationRootPageComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: HomeComponent},
      {path: 'resume', component: ResumePageComponent},
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
