import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthorComponent } from './admin/author/author.component';
import { BookComponent } from './admin/book/book.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './models';


const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin/authors', component: AuthorComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: 'admin/books', component: BookComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
