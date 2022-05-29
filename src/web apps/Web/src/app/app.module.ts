import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularDraggableModule } from 'angular2-draggable';
import { TextMaskModule } from 'angular2-text-mask';
import { NgBrazil } from 'ng-brazil';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CustomFormsModule } from 'ng2-validation';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './telas/telas-principais/login/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LoginService } from './telas/telas-principais/login/services/login-service';
import { NotFoundComponent } from './telas/telas-apoio/not-found/not-found.component';
import { RegistroComponent } from './telas/telas-principais/login/registro/registro.component';
import { ToastrModule } from 'ngx-toastr';
import { RodapeLoginComponent } from './telas/telas-principais/login/rodape-login/rodape-login.component';
import { MenuLoginComponent } from './telas/telas-principais/login/menu-login/menu-login.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { AcessoNegadoComponent } from './telas/telas-apoio/acesso-negado/acesso-negado.component';
import { registerLocaleData } from '@angular/common';
import localePT from '@angular/common/locales/pt';
import { BaseService } from './services/base.service';
registerLocaleData(localePT);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistroComponent,
    NotFoundComponent,
    MenuLoginComponent,
    RodapeLoginComponent,
    AcessoNegadoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    RouterModule,
    NgbModule,
    FormsModule,
    HttpClientModule,    
    ReactiveFormsModule,
    NgBrazil,
    TextMaskModule,
    CustomFormsModule,
    CurrencyMaskModule,
    AngularDraggableModule,
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
    CollapseModule.forRoot(),
  ],
  exports: [
    NotFoundComponent,
    AcessoNegadoComponent,
    MenuLoginComponent,
    RodapeLoginComponent,
  ],
  providers: [HttpClient, LoginService, BaseService, {provide: LOCALE_ID, useValue: 'pt-br'}],
  bootstrap: [AppComponent]
})
export class AppModule { }
