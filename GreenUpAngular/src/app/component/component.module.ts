import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { FooterComponent } from './footer/footer.component';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
    imports: [
      CommonModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatButtonModule
    ],
    declarations: [
        ToolbarComponent,
        CardComponent,
        FooterComponent,
    ],
    exports: [CardComponent, ToolbarComponent, FooterComponent] 
})
export class ComponentModule {}