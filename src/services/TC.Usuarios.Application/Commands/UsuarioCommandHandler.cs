using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TC.Usuarios.Application.Events;
using TC.Usuarios.Domain;
using TC.Core.Messages;

namespace TC.Usuarios.Application.Commands
{
    public class UsuarioCommandHandler : CommandHandler,
        IRequestHandler<AdicionarUsuarioCommand, ValidationResult>,
        IRequestHandler<AtualizarUsuarioCommand, ValidationResult>,
        IRequestHandler<ExcluirUsuarioCommand, ValidationResult>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var usuario = new Usuario(message.Nome, message.Email, message.DataNascimento, message.Escolaridade);

            var usuarioExistente = await _usuarioRepository.ObterPorEmail(usuario.Email);

            if (usuarioExistente != null)
            {
                AdicionarErro("Este email já está em uso por outro usuario.");
                return ValidationResult;
            }

            _usuarioRepository.Adicionar(usuario);

            usuario.AdicionarEvento(new UsuarioRegistradoEvent(message.Nome, message.Email));

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var usuarioExistente = await _usuarioRepository.ObterPorId(message.Id);

            if (usuarioExistente == null)
            {
                AdicionarErro("Usuário não encontrado.");
                return ValidationResult;
            }

            var usuario = new Usuario(message.Nome, message.Email, message.DataNascimento, message.Escolaridade)
            {
                Id = message.Id
            };

            _usuarioRepository.Atualizar(usuario);

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }        

        public async Task<ValidationResult> Handle(ExcluirUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var usuario = await _usuarioRepository.ObterPorId(message.Id);

            if (usuario == null)
            {
                AdicionarErro("Usuário não encontrado.");
                return ValidationResult;
            }

            _usuarioRepository.Excluir(usuario);

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }        
    }
}