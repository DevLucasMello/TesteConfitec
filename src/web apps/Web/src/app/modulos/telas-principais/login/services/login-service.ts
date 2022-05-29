import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { UsuarioRespostaLogin } from "../models/login-resposta";
import { UsuarioLogin, UsuarioRegistro } from "../models/login-envio";
import { catchError, map } from "rxjs/operators";
import { BaseService } from "src/app/servicos/base.service";

@Injectable()
export class LoginService {

    constructor(private http: HttpClient, private base: BaseService) { }
    
    login(usuario: UsuarioLogin): Observable<UsuarioRespostaLogin> {
        let response = this.http
            .post(`${this.base.UrlServiceCrud}autenticar`,usuario, this.base.ObterHeaderJson())
            .pipe(
                map(this.base.extractData),
                catchError(this.base.serviceError));

        return response;        
    }

    registro(usuario: UsuarioRegistro): Observable<UsuarioRespostaLogin> {
        let response = this.http
            .post(`${this.base.UrlServiceCrud}nova-conta`,usuario, this.base.ObterHeaderJson())
            .pipe(
                map(this.base.extractData),
                catchError(this.base.serviceError));

        return response;
    }    
}