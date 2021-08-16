import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';

// autoinject JWT into all requests
import { JwtInterceptor } from './_helpers/jwt.interceptor';

import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { AuthorComponent } from './admin/author/author.component';
import { BookComponent } from './admin/book/book.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    AuthorComponent,
    BookComponent,
    LoginComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
