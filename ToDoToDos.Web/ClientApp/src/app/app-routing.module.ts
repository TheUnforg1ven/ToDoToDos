import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
 
import { MissionsComponent } from './missions/missions.component';
import { LandingComponent } from './landing/landing.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
 
const routes: Routes = [
  { path: '', redirectTo: '/landing', pathMatch: 'full' },
  { path: 'missions', component: MissionsComponent },
  { path: 'landing', component: LandingComponent },
  { path: 'user', component: UserComponent, 
    children: [
      { path: 'registration', component : RegistrationComponent },
      { path: 'login', component : LoginComponent },
    ] 
  },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
];
 
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}