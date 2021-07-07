import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthorComponent } from './admin/author/author.component';


const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'admin/authors', component: AuthorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
