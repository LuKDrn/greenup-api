import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { HomePage } from './home.page';

import { HomePageRoutingModule } from './home-routing.module';
import { MenuComponentModule } from '../menu/menu.module';
import { ToolbarComponent } from '../component/toolbar/toolbar.component';
import { CardComponent } from '../component/card/card.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    HomePageRoutingModule,
    MenuComponentModule
  ],
  declarations: [
    HomePage,
    ToolbarComponent,
    CardComponent
  ]
})
export class HomePageModule {}
