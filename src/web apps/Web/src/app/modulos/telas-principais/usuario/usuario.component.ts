import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-usuario',  
  template: '<app-menu-autenticado></app-menu-autenticado><router-outlet></router-outlet><app-rodape-autenticado></app-rodape-autenticado>'
})
export class UsuarioComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}