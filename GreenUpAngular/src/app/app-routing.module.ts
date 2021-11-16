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
import { AddMissionComponent } from './modules/mission/add-mission/add-mission.component';
import { BlogComponent } from './modules/blog/blog/blog.component';
import { ListBlogComponent } from './modules/blog/list-blog/list-blog.component';

const routes: Routes = [
  //HOME
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
  //USER
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
  //MISSIONS
  {
    path: 'missions',
    component: MissionsComponent
  },
  {
    path: 'mission/:id',
    component: MissionIdComponent // MissionIdComponent
  },
  //PRODUITS BIO
  {
    path: 'produits',
    component: ProduitsComponent
  },
  {
    path: 'produit/:id',
    component: ProduitIdComponent
  },
  //CHALLENGES
  {
    path: 'challenges',
    component: ChallengesComponent
  },
  //ASSOCIATION
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
  //BLOG
  {
    path: 'blog',
    component: ListBlogComponent
  },
  {
    path: 'blog/:id',
    component: BlogComponent
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