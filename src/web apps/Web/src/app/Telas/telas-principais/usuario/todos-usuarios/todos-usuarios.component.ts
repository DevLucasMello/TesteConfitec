import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ListaDados } from 'src/app/models/lista-dados';
import { Paginacao } from 'src/app/models/paginacao';
import { Usuario } from '../models/usuario';
import { UsuarioService } from '../services/usuario.service';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-todos-usuarios',
  templateUrl: './todos-usuarios.component.html',
  styleUrls: ['./todos-usuarios.component.css']
})
export class TodosUsuariosComponent implements OnInit {  

  public paginacao: Paginacao = new Paginacao();
  
  public dados: ListaDados<Usuario>;
  public usuarioSelecionado: Usuario;
  public filtroNome: string;

  @ViewChild('mdExcluir', { static: true })
  public mdExcluir: any;

  constructor(
    private usuarioService: UsuarioService, 
    private router: Router, 
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.carregarRegistros();
  }

  obterTodosUsuarios(page: number = 1, take: number = 8, query: string = ''){
    this.usuarioService.obterTodosUsuarios(page, take, query)
      .subscribe(response => {
        if (response){
          this.dados = response;
          this.variaveisPaginacao(this.dados);         
        }                
      });      
  }

  public carregarRegistros(event: any = 1){
    if(this.filtroNome !== undefined && this.filtroNome !== ''){
      this.obterTodosUsuarios(event, 8, this.filtroNome);
    }
    else{
      this.obterTodosUsuarios(event);
    }            
  }  

  private variaveisPaginacao(dados: ListaDados<Usuario>){    
    this.paginacao.pageIndex = dados.pageIndex;
    this.paginacao.pageSize = dados.pageSize;
    this.paginacao.totalResults = dados.totalResults;
  }

  public cadastrarUsuario(){
    this.router.navigate(['cadastrar'], { relativeTo: this.route});
  }

  public editarUsuario(id: number){
    this.router.navigate(['editar', id], { relativeTo: this.route});
  }

  abrirExclusao(id: number): void{
    this.dados.list.forEach(x => {
      if(x.id == id) {
        this.preencherDadoUsuarioSelecionado(x);
      }
    });    
    this.mdExcluir.show();
  }

  preencherDadoUsuarioSelecionado(x: Usuario){
    this.usuarioSelecionado = new Usuario();
        this.usuarioSelecionado.id = x.id;
        this.usuarioSelecionado.primeiroNome = x.primeiroNome;
        this.usuarioSelecionado.ultimoNome = x.ultimoNome;
        this.usuarioSelecionado.escolaridade = x.escolaridade;
        this.usuarioSelecionado.dataNascimento = x.dataNascimento;
        this.usuarioSelecionado.email = x.email;
        this.usuarioSelecionado.dataNascimento = this.converterDataNascimento(x.dataNascimento)
  }

  public converterDataNascimento(data: string): string{
    var datePipe = new DatePipe('pt-br');
    return datePipe.transform(data, 'dd/MM/yyyy');
  }

  excluirUsuario(){    
    this.usuarioService.excluirUsuario(this.usuarioSelecionado.id)
      .subscribe(response => {
        if (response){
          this.mdExcluir.hide();
          setTimeout(() => {document.location.reload();}, 3000);
          this.toastr.success('Usuário excluído com sucesso!');          
        }
        else{
          this.toastr.error('Erro ao excluir usuário');
        }
      })
  }
  
}
