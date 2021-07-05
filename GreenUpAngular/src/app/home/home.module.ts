import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ComponentModule } from '../component/component.module';
import { HomeComponent } from './home.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ComponentModule,
  ],
  declarations: [
    HomeComponent,
  ],
  exports: [HomeComponent]
})
export class HomeModule {}