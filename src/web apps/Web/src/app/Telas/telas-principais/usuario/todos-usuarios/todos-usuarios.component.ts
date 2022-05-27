import { Component, OnInit } from '@angular/core';
import { ListaDados } from 'src/app/Models/lista-dados';
import { Paginacao } from 'src/app/Models/paginacao';
import { Usuario } from '../models/usuario';
import { UsuarioService } from '../services/usuario.service';

@Component({
  selector: 'app-todos-usuarios',
  templateUrl: './todos-usuarios.component.html',
  styleUrls: ['./todos-usuarios.component.css']
})
export class TodosUsuariosComponent implements OnInit {  

  public paginacao: Paginacao = new Paginacao();
  
  public dados: ListaDados<Usuario[]>;
  public filtroPlaca: string;

  constructor(private usuarioService: UsuarioService) { }

  ngOnInit() {
    this.carregarRegistros();
  }

  obterTodosUsuarios(page: number = 1, take: number = 8){
    this.usuarioService.obterTodosUsuarios(page, take)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);         
        }                
      });      
  }

  public carregarRegistros(event: any = 1){    
    this.obterTodosUsuarios(event);            
  }  

  private variaveisPaginacao(dados: ListaDados<Usuario[]>){    
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }

}
