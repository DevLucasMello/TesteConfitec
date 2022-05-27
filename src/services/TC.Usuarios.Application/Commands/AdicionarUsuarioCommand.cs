using FluentValidation;
using System;
using TC.Usuarios.Application.Messages;
using TC.Core.DomainObjects;
using TC.Core.Messages;
using TC.Core.Utils;

namespace TC.Usuarios.Application.Commands
{
    public class AdicionarUsuarioCommand : Command
    {
        public Nome Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public Escolaridade Escolaridade { get; private set; }

        public AdicionarUsuarioCommand(Nome nome, string email, DateTime? dataNascimento, Escolaridade escolaridade)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarUsuarioValidation : AbstractValidator<AdicionarUsuarioCommand>
    {        
        public AdicionarUsuarioValidation()
        {
            RuleFor(u => u.Nome.PrimeiroNome)
                .NotEmpty()
                .WithMessage(UsuarioCommandErrorMessages.PrimeiroNomeNuloErroMsg)
                .Length(3, 150)
                .WithMessage(UsuarioCommandErrorMessages.PrimeiroNomeQtdErroMsg);

            RuleFor(u => u.Nome.UltimoNome)
                .NotEmpty()
                .WithMessage(UsuarioCommandErrorMessages.UltimoNomeNuloErroMsg)
                .Length(3, 150)
                .WithMessage(UsuarioCommandErrorMessages.UltimoNomeQtdErroMsg);

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage(UsuarioCommandErrorMessages.EmailNuloErroMsg)
                .EmailAddress()
                .WithMessage(UsuarioCommandErrorMessages.EmailInvalidoErroMsg);

            RuleFor(u => u.DataNascimento)
                .NotEmpty()
                .WithMessage(UsuarioCommandErrorMessages.DataNascimentoNuloErroMsg)
                .Must(MethodsUtils.UsuarioIdadeAtual)
                .WithMessage(UsuarioCommandErrorMessages.DataNascimentoMaiorDataAtualErroMsg);

            RuleFor(u => u.Escolaridade)
                .NotEmpty()
                .WithMessage(UsuarioCommandErrorMessages.EscolaridadeNuloErroMsg)
                .Must(MethodsUtils.EscolaridadeDiferentePermitida)
                .WithMessage(UsuarioCommandErrorMessages.EscolaridadeNaoPermitidaErroMsg);
        }
    }
}