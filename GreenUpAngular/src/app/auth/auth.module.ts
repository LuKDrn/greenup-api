import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ComponentModule } from '../component/component.module';
import { AuthComponent } from './auth.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ComponentModule,
  ],
  declarations: [
    AuthComponent,
  ],
  exports: [AuthComponent]
})
export class AuthModule {}