import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DisplayMessage, GenericValidator, ValidationMessages } from 'src/app/Validacoes/generic-form-validator';
import { UsuarioService } from '../services/usuario.service';

@Component({
  selector: 'app-detalhe-usuario',
  templateUrl: './detalhe-usuario.component.html',
  styleUrls: ['./detalhe-usuario.component.css']
})
export class DetalheUsuarioComponent implements OnInit {

  public idUsuario: number = 0;
  public novoCadastro: boolean = true;

  cadastroUsuarioForm: FormGroup; 
  
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private usuarioService: UsuarioService
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
  }

}
