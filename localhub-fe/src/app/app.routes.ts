import { Routes } from '@angular/router';
import { AuthenticationPageComponent } from './components/authentication-page/authentication-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';
//import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { AuthGuard } from './shared/core/auth-guard.core';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AuthenticationPageComponent,
  },
  { path: '', component: MainPageComponent, canActivate: [AuthGuard] },
  {
    path: 'profilePage',
    component: ProfilePageComponent,
    canActivate: [AuthGuard],
  },
  //{ path: '**', component: PageNotFoundComponent, canActivate: [AuthGuard] },
];
