import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { UsersComponent } from './users/users.component';
import { InfiniteScrollComponent } from './infinite-scroll/infinite-scroll.component';
import { LayoutComponent } from './layout/layout.component';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'inf-scroll', component: InfiniteScrollComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
