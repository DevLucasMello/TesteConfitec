import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/Validacoes/generic-form-validator';
import { UsuarioService } from '../services/usuario.service';
import { fromEvent, merge, Observable } from 'rxjs';
import { Usuario } from '../models/usuario';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-detalhe-usuario',
  templateUrl: './detalhe-usuario.component.html',
  styleUrls: ['./detalhe-usuario.component.css']
})
export class DetalheUsuarioComponent implements OnInit, AfterViewInit {

  public idUsuario: number = 0;
  public novoCadastro: boolean = true;
  public usuario: Usuario;

  @ViewChildren(FormControlName, {read: ElementRef}) forInputElements: ElementRef[];

  cadastroUsuarioForm: FormGroup; 
  
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private usuarioService: UsuarioService,
    private toastr: ToastrService
    ) {
      this.validationMessages = {
        primeiroNome: {
          required: 'O primeiro nome é requerido',
          minlength: 'O primeiro nome precisa ter no mínimo 2 caracteres',
          maxlength: 'O primeiro nome precisa ter no máximo 150 caracteres'
        },
        ultimoNome: {
          required: 'O ultimo nome é requerido',
          minlength: 'O ultimo nome precisa ter no mínimo 2 caracteres',
          maxlength: 'O ultimo nome precisa ter no máximo 150 caracteres'
        },
        escolaridade: {
          required: 'A escolaridade é requerida',
          minlength: 'A escolaridade precisa ter no mínimo 2 caracteres',
          maxlength: 'A escolaridade precisa ter no máximo 150 caracteres'
        },
        dataNascimento: {
          required: 'A data de nascimento é requerida',
          minlength: 'A data de nascimento precisa ter no mínimo 6 caracteres',
          maxlength: 'A data de nascimento precisa ter no máximo 150 caracteres'
        },
        email: {
          required: 'O email é requerido',
          minlength: 'O email precisa ter no mínimo 8 caracteres',
          maxlength: 'O email precisa ter no máximo 150 caracteres'
        }
      };
      this.genericValidator = new GenericValidator(this.validationMessages);
    
    }

  ngOnInit() {
    this.idUsuario = parseInt(this.route.snapshot.params['id']);

    if(this.idUsuario > 0){
      this.obterPorId(this.idUsuario);      
      this.novoCadastro = false;
    }

    this.cadastroUsuarioForm = this.fb.group({
      primeiroNome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      ultimoNome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      escolaridade: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      dataNascimento: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(150)]],
      email: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(150)]]      
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.forInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.cadastroUsuarioForm);
    });
  }

  salvarUsuario(){
    if(this.cadastroUsuarioForm.dirty && this.cadastroUsuarioForm.valid){
      this.usuario = Object.assign({}, this.usuario, this.cadastroUsuarioForm.value);
      this.usuario.dataNascimento = this.formatarDataNascimento(this.usuario.dataNascimento)
      if (this.novoCadastro){        
        this.cadastrarUsuario(this.usuario); 
      }
      else{ this.atualizarUsuario(this.usuario); }      
    }
  }

  voltarDashboard(){
    this.router.navigate(['usuario']);
  }

  preencherForm(usuario: Usuario) {
    this.cadastroUsuarioForm.patchValue({      
      primeiroNome: usuario.primeiroNome,
      ultimoNome: usuario.ultimoNome,
      escolaridade: usuario.escolaridade,
      dataNascimento: usuario.dataNascimento,
      email: usuario.email
    });
  }

  public obterPorId(id: number){
    this.usuarioService.obterPorId(id)
      .subscribe(response => {
        if (response){
          this.usuario = response;
          this.usuario.dataNascimento = this.converterDataNascimento(this.usuario.dataNascimento);          
          this.preencherForm(this.usuario);
        }
      })
  }

  public atualizarUsuario(usuario: Usuario){
    this.usuarioService.atualizarUsuario(usuario, this.idUsuario)
      .subscribe(response => {
        if (response){
          this.voltarDashboard();
          this.toastr.success('Usuário alterado com sucesso!');
        }else{
          this.toastr.error('Erro ao atualizar usuário');
        }
      })
  }

  public cadastrarUsuario(usuario: Usuario){
    this.usuarioService.cadastrarUsuario(usuario)
      .subscribe(response => {
        if (response){                    
          this.voltarDashboard();
          this.toastr.success('Usuário cadastrado com sucesso!');
        }else{
          this.toastr.error('Erro ao cadastrar usuário');
        }
      })
  }

  public converterDataNascimento(data: string): string{
    var datePipe = new DatePipe('pt-br');
    return datePipe.transform(data, 'dd/MM/yyyy');
  }

  formatarDataNascimento(data: string): string{
    let dia = parseInt(data.substring(0,2));
    let mes = parseInt(data.substring(3,5));
    let ano = parseInt(data.substring(6,10));
    return ano + "-" + mes + "-" + dia;
  }

}
