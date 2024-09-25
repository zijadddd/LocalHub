import { Routes } from '@angular/router';
import { AuthenticationPageComponent } from './components/authentication-page/authentication-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';
//import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { AuthGuard } from './shared/core/auth-guard.core';
import { ProfilePageComponent } from './components/profile-page/profile-page.component';
import { PostPageComponent } from './components/post-page/post-page.component';
import { AddPostPageComponent } from './components/add-post-page/add-post-page.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AuthenticationPageComponent,
  },
  { path: '', component: MainPageComponent, canActivate: [AuthGuard] },
  {
    path: 'profilePage/:id',
    component: ProfilePageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'postPage/:id',
    component: PostPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'addPost',
    component: AddPostPageComponent,
    canActivate: [AuthGuard],
  },
  //{ path: '**', component: PageNotFoundComponent, canActivate: [AuthGuard] },
];
