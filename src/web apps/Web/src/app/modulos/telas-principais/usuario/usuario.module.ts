import { CommonModule } from "@angular/common";
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AngularDraggableModule } from "angular2-draggable";
import { NgBrazil } from "ng-brazil";
import { CustomFormsModule } from "ng2-validation";
import { CollapseModule } from "ngx-bootstrap/collapse";
import { ModalModule } from "ngx-bootstrap/modal";
import { AutenticadoModule } from "../../telas-apoio/autenticado-module";
import { UsuarioComponent } from "./usuario.component";
import { UsuarioRoutingModule } from "./usuario.route";
import { UsuarioGuard } from "./services/usuario.guard";
import { UsuarioService } from "./services/usuario.service";
import { TodosUsuariosComponent } from "./todos-usuarios/todos-usuarios.component";
import { DetalheUsuarioComponent } from "./detalhe-usuario/detalhe-usuario.component";
import { TextMaskModule } from "angular2-text-mask";
import { ErrorInterceptor } from "src/app/utilidades/error.handler.service";
import { BaseService } from "src/app/servicos/base.service";

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
];

@NgModule({
  declarations: [
    UsuarioComponent,
    TodosUsuariosComponent,
    DetalheUsuarioComponent
  ],
  imports: [
    CommonModule, 
    CollapseModule.forRoot(),
    RouterModule,
    NgbModule,
    FormsModule,
    UsuarioRoutingModule,
    ReactiveFormsModule,
    NgBrazil,
    CustomFormsModule,
    AngularDraggableModule,
    ModalModule.forRoot(),
    AutenticadoModule,
    HttpClientModule,
    TextMaskModule
  ],
  providers: [
    httpInterceptorProviders, 
    UsuarioGuard, 
    UsuarioService, 
    HttpClient,
    BaseService
  ]
})
export class UsuarioModule { }