import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { MaterialModule } from './material.module';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { FeedScrollComponent } from './feed-scroll/feed-scroll.component';
import { LayoutComponent } from './layout/layout.component';
import { NavigationComponent } from './layout/navigation/navigation.component';
import { ProfileComponent } from './profile/profile.component';
import { UserControlsComponent } from './layout/user-controls/user-controls.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { SignupComponent } from './signup/signup.component';

import { AuthenticationService } from './_services/authentication.service';
import { appInitializer } from './_helpers/app.initializer';
import { HttpRequestInterceptor} from './_helpers/http-request.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    FeedScrollComponent,
    LayoutComponent,
    NavigationComponent,
    ProfileComponent,
    UserControlsComponent,
    WelcomeComponent,
    SignupComponent
  ],
  imports: [
    MaterialModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    InfiniteScrollModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService] },
    { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
