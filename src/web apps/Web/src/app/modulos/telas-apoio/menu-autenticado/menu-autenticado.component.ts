import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/utilidades/localStorage';
import { Nav } from './nav';

@Component({
  selector: 'app-menu-autenticado',
  templateUrl: './menu-autenticado.component.html',
  styleUrls: ['./menu-autenticado.component.css']
})
export class MenuAutenticadoComponent implements OnInit {

  public localStorage: LocalStorageUtils = new LocalStorageUtils();

  isCollapsed = true;

  nav: Nav[] = [
    {
      link: '/usuario',
      name: 'Usuarios',
      exact: false,
      home: false
    }
  ];

  constructor(private router: Router) { }

  ngOnInit() {
  }

  logout() {
    this.localStorage.limparDadosLocais();
    this.router.navigate(['']);
  }

}