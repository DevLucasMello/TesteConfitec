import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/utilidades/localStorage';

@Injectable()
export class UsuarioGuard implements CanActivate {

    localStorageUtils = new LocalStorageUtils();

    constructor(protected router: Router) {}

    canActivate(routeAc: ActivatedRouteSnapshot) {
        if(!this.localStorageUtils.obterToken()){
            this.router.navigate(['./login'], { queryParams: { returnUrl: "/" + routeAc.url }});
        }

        return true;
    }
}