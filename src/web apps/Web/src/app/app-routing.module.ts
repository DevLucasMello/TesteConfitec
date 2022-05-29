import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AcessoNegadoComponent } from './modulos/telas-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from './modulos/telas-apoio/not-found/not-found.component';
import { LoginComponent } from './modulos/telas-principais/login/login/login.component';
import { RegistroComponent } from './modulos/telas-principais/login/registro/registro.component';

const routes: Routes = [

  { path: '', component: LoginComponent},

  { path: 'login', component: LoginComponent},

  { path: 'registro', component: RegistroComponent},

  { path: 'home', loadChildren: () => import('./modulos/telas-principais/home/home-module').then(m => m.HomeModule) },

  { path: 'usuario', loadChildren: () => import('./modulos/telas-principais/usuario/usuario.module').then(m => m.UsuarioModule) },

  { path: 'acesso-negado', component: AcessoNegadoComponent },
  
  { path: 'nao-encontrado', component: NotFoundComponent },

  { path: '**', component: NotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
