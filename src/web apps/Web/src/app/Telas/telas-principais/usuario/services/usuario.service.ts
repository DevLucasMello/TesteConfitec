import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/services/base.service';
import { catchError } from "rxjs/operators";
import { Usuario } from "../models/usuario";
import { ListaDados } from "src/app/Models/lista-dados";

@Injectable()
export class UsuarioService extends BaseService {

    constructor(private http: HttpClient) { super() }
    
    obterTodosUsuarios(page: number, take: number): Observable<ListaDados<Usuario[]>> {        
        return this.http
            .get<ListaDados<Usuario[]>>(`${this.UrlServiceCrud}usuario?ps=${take}&page=${page}`, super.ObterAuthHeaderJson())
            .pipe(catchError(super.serviceError));
    }
}