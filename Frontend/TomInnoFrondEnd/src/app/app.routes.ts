import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { HomeComponent } from './pages/home.component/home.component';
import { LoginComponent } from './pages/login.component/login.component';
import { RegisterComponent } from './pages/register.component/register.component';
import { ListingDetailComponent } from './pages/listing-detail.component/listing-detail.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'listing/:id', component: ListingDetailComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '' },
];
