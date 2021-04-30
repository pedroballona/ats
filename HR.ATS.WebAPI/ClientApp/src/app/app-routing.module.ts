import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ResumePageComponent } from './pages/resume-page/resume-page.component';

const routes: Routes = [
  {path: 'resume', component: ResumePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
