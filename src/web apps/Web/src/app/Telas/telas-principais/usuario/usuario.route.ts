import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AcessoNegadoComponent } from '../../telas-apoio/acesso-negado/acesso-negado.component';
import { NotFoundComponent } from '../../telas-apoio/not-found/not-found.component';
import { UsuarioComponent } from './usuario.component';
import { TodosUsuariosComponent } from './todos-usuarios/todos-usuarios.component';
import { UsuarioGuard } from './services/usuario.guard';

const usuarioRouterConfig: Routes = [
  {
    path: '', component: UsuarioComponent,
    children: [

      { path: '', component: TodosUsuariosComponent, canActivate: [UsuarioGuard] },

      { path: 'usuario', component: TodosUsuariosComponent, canActivate: [UsuarioGuard] },

      { path: 'acesso-negado', component: AcessoNegadoComponent },
      
      { path: '**', component: NotFoundComponent }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(usuarioRouterConfig)
  ],
  exports: [RouterModule]
})
export class UsuarioRoutingModule { }