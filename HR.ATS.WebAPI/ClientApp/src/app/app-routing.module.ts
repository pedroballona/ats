import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ApplicationRootPageComponent } from './pages/application-root-page/application-root-page.component';

const routes: Routes = [
  {path: '', component: ApplicationRootPageComponent, canActivate: [AuthGuard], canActivateChild: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
