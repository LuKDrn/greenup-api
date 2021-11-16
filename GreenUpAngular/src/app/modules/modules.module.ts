import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from './user/user.service';
import { ProduitsService } from './produit/produits.service';
import { MissionsService } from './mission/missions.service';
import { ChallengesService } from './challenges/challenges.service';
import { AssociationsService } from './association/associations.service';
import { AuthComponent } from './user/auth/auth.component';
import { SignUpComponent } from './user/sign-up/sign-up.component';
import { MissionsComponent } from './mission/missions/missions.component';
import { ProduitsComponent } from './produit/produits/produits.component';
import { ChallengesComponent } from './challenges/challenges.component';
import { AssociationsComponent } from './association/associations/associations.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCarouselModule } from '@ngmodule/material-carousel';
import { AppRoutingModule } from '../app-routing.module';
import { ComponentModule } from '../component/component.module';
import { AssociationIdComponent } from './association/association-id/association-id.component';
import { MissionIdComponent } from './mission/mission-id/mission-id.component';
import { ProduitIdComponent } from './produit/produit-id/produit-id.component';
import { EditProfileComponent } from './user/edit-profile/edit-profile.component';
import { AssociationAuthComponent } from './association/association-auth/association-auth.component';
import { AssociationSignUpComponent } from './association/association-sign-up/association-sign-up.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AddMissionComponent } from './mission/add-mission/add-mission.component';
import { ListBlogComponent } from './blog/list-blog/list-blog.component';
import { BlogComponent } from './blog/blog/blog.component';
import { BlogService } from './blog/blog.service';

@NgModule({
  declarations: [
    AuthComponent,
    SignUpComponent,
    MissionsComponent,
    MissionIdComponent,
    ProduitsComponent,
    ProduitIdComponent,
    ChallengesComponent,
    AssociationsComponent,
    AssociationIdComponent,
    HomeComponent,
    EditProfileComponent,
    AssociationAuthComponent,
    AssociationSignUpComponent,
    AddMissionComponent,
    ListBlogComponent,
    BlogComponent
  ],
  imports: [
    MatCarouselModule,
    CommonModule,
    AppRoutingModule, 
    HttpClientModule, 
    FormsModule,
    ComponentModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    CommonModule,
    MatSnackBarModule
  ],
  providers: [ProduitsService, MissionsService, ChallengesService, AssociationsService, UserService, BlogService],
})
export class ModulesModule { }
