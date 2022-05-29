import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseService } from 'src/app/services/base.service';
import { catchError } from "rxjs/operators";
import { Usuario } from "../models/usuario";
import { ListaDados } from "src/app/models/lista-dados";

@Injectable()
export class UsuarioService {

    constructor(private http: HttpClient, private base: BaseService) {  }
    
    obterTodosUsuarios(page: number, take: number, query: string): Observable<ListaDados<Usuario>> {        
        return this.http
            .get<ListaDados<Usuario>>(`${this.base.UrlServiceCrud}usuario?ps=${take}&page=${page}&q=${query}`, this.base.ObterAuthHeaderJson())
            .pipe(catchError(this.base.serviceError));
    }

    obterPorId(id: number): Observable<Usuario> {
        return this.http
            .get<Usuario>(this.base.UrlServiceCrud + "usuario/" + id, this.base.ObterAuthHeaderJson())
            .pipe(catchError(this.base.serviceError));
    }

    cadastrarUsuario(usuario: Usuario): Observable<any> {
        return this.http.post<any>(`${this.base.UrlServiceCrud}usuario`,usuario, this.base.ObterAuthHeaderJson());
    }

    atualizarUsuario(usuario: Usuario, id: number): Observable<any> {        
        return this.http.put<any>(`${this.base.UrlServiceCrud}usuario/${id}`,usuario, this.base.ObterAuthHeaderJson());
    }

    excluirUsuario(id: number): Observable<any> {        
        return this.http.delete<any>(`${this.base.UrlServiceCrud}usuario/${id}`, this.base.ObterAuthHeaderJson());
    }
}