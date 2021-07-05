import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedService } from './shared.service';
import { AuthComponent } from './auth/auth.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ComponentModule } from './component/component.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { HomeModule } from './home/home.module';
import { HomeComponent } from './home/home.component';
import { AuthModule } from './auth/auth.module';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent, 
  ],
  entryComponents: [],
  imports: [
    BrowserModule, 
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    }),
    AppRoutingModule, 
    HttpClientModule, 
    FormsModule,
    ComponentModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    HomeModule,
    AuthModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent],
})
export class AppModule {}
