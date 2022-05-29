import { HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { environment } from 'src/environments/environment';
import { LocalStorageUtils } from "../utilidades/localStorage";

@Injectable()
export class BaseService {

    public localStorage: LocalStorageUtils = new LocalStorageUtils();
    
    public UrlServiceCrud: string = environment.apiCrud;

    public ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    public ObterAuthHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.localStorage.obterToken()}`
            })
        };
    }    

    public extractData(response: any) {
        return response || {};
    }

    public serviceError(response: Response | any) {
        let customError: string[] = [];
        let customResponse = { error: { errors: [] }}

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }
        if (response.status === 500) {
            customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");            
                           
            customResponse.error.errors = customError;
            return throwError(customResponse);
        }

        console.error(response);
        return throwError(response);
    }
}
