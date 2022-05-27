using FluentValidation;
using TC.Usuarios.Application.Messages;
using TC.Core.Messages;

namespace TP.Condutores.Application.Commands
{
    public class ExcluirUsuarioCommand : Command
    {
        public int Id { get; set; }

        public ExcluirUsuarioCommand(int id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new ExcluirUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class ExcluirUsuarioValidation : AbstractValidator<ExcluirUsuarioCommand>
        {
            public ExcluirUsuarioValidation()
            {
                RuleFor(c => c.Id)
                    .NotEmpty()
                    .WithMessage(UsuarioCommandErrorMessages.IdNuloErroMsg);
            }
        }
    }
}
