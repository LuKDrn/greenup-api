import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import { RouteReuseStrategy, RouterModule } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { MenuComponentModule } from './menu/menu.module';
import {SharedService} from './shared.service';
import { AuthComponent } from './auth/auth.component';
import { JwtModule } from '@auth0/angular-jwt';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent, 
    AuthComponent
  ],
  entryComponents: [],
  imports: [
    BrowserModule, 
    RouterModule.forRoot([
      { path: '', component: AppComponent },
      { path: 'auth', component: AuthComponent },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    }),
    AppRoutingModule, 
    MenuComponentModule, 
    HttpClientModule, 
    FormsModule
  ],
  providers: [SharedService, { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent],
})
export class AppModule {}
