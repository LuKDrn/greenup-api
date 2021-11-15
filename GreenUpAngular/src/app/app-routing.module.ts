import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './modules/user/auth/auth.component';
import { HomeComponent } from './modules/home/home.component';
import { SignUpComponent } from './modules/user/sign-up/sign-up.component';
import { MissionsComponent } from './modules/mission/missions/missions.component';
import { ProduitsComponent } from './modules/produit/produits/produits.component';
import { ChallengesComponent } from './modules/challenges/challenges.component';
import { AssociationsComponent } from './modules/association/associations/associations.component';
import { AssociationIdComponent } from './modules/association/association-id/association-id.component';
import { MissionIdComponent } from './modules/mission/mission-id/mission-id.component';
import { ProduitIdComponent } from './modules/produit/produit-id/produit-id.component';
import { EditProfileComponent } from './modules/user/edit-profile/edit-profile.component';
import { AssociationAuthComponent } from './modules/association/association-auth/association-auth.component';
import { AssociationSignUpComponent } from './modules/association/association-sign-up/association-sign-up.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'home',
        component: HomeComponent
      }
    ]
  },
  { 
    path : 'auth',
    component: AuthComponent
  },
  {
    path: 'signUp',
    component: SignUpComponent
  },
  {
    path: 'edit-profile',
    component: EditProfileComponent
  },
  {
    path: 'missions',
    component: MissionsComponent
  },
  {
    path: 'mission/:id',
    component: MissionIdComponent
  },
  {
    path: 'produits',
    component: ProduitsComponent
  },
  {
    path: 'produit/:id',
    component: ProduitIdComponent
  },
  {
    path: 'challenges',
    component: ChallengesComponent
  },
  {
    path: 'associations',
    component: AssociationsComponent
  },
  {
    path: 'association/:id',
    component: AssociationIdComponent
  },
  {
    path: 'associations/auth',
    component: AssociationAuthComponent
  },
  {
    path: 'associations/signUp',
    component: AssociationSignUpComponent
  },
  {
    path :'**',
    pathMatch: 'full',
    component: PagenotfoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}