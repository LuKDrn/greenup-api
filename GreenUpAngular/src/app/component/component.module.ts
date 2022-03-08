import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { FooterComponent } from './footer/footer.component';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { FilterComponent } from './filter/filter.component';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { CardBlogComponent } from './card-blog/card-blog.component';
import { ForgotIdentifiantComponent } from './dialog/forgot-identifiant/forgot-identifiant.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DialogTextComponent } from './dialog/dialog-text/dialog-text.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      MatToolbarModule,
      MatIconModule,
      MatCardModule,
      MatButtonModule,
      MatMenuModule,
      MatSelectModule,
      MatCheckboxModule,
      MatFormFieldModule,
      MatInputModule,
      MatDividerModule,
      MatChipsModule,
      MatDialogModule
    ],
    declarations: [
        ToolbarComponent,
        CardComponent,
        FooterComponent,
        FilterComponent,
        CardBlogComponent,
        ForgotIdentifiantComponent,
        DialogTextComponent,
    ],
    exports: [CardComponent, ToolbarComponent, FooterComponent, FilterComponent, CardBlogComponent] 
})
export class ComponentModule {}