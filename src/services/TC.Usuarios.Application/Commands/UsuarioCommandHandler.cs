//using FluentValidation.Results;
//using MediatR;
//using System.Threading;
//using System.Threading.Tasks;
//using TP.Condutores.Application.Events;
//using TP.Condutores.Domain;
//using TP.Core.Messages;

//namespace TP.Condutores.Application.Commands
//{
//    public class UsuarioCommandHandler : CommandHandler,
//        IRequestHandler<AdicionarUsuarioCommand, ValidationResult>,
//        IRequestHandler<AtualizarUsuarioCommand, ValidationResult>,
//        IRequestHandler<AtualizarVeiculoCondutorCommand, ValidationResult>,
//        IRequestHandler<ExcluirUsuarioCommand, ValidationResult>,
//        IRequestHandler<ExcluirVeiculoCondutorCommand, ValidationResult>
//    {
//        private readonly ICondutorRepository _condutorRepository;

//        public UsuarioCommandHandler(ICondutorRepository condutorRepository)
//        {
//            _condutorRepository = condutorRepository;
//        }

//        public async Task<ValidationResult> Handle(AdicionarUsuarioCommand message, CancellationToken cancellationToken)
//        {
//            if (!message.EhValido()) return message.ValidationResult;

//            var condutor = new Condutor(message.Nome, message.CPF, message.Telefone, message.Email, message.CNH, message.DataNascimento);

//            var condutorExistente = await _condutorRepository.ObterPorCPF(condutor.CPF);

//            if (condutorExistente != null)
//            {
//                AdicionarErro("Este CPF já está em uso por outro condutor.");
//                return ValidationResult;
//            }

//            _condutorRepository.Adicionar(condutor);

//            condutor.AdicionarEvento(new CondutorRegistradoEvent(message.Nome, message.CPF, message.Email));

//            return await PersistirDados(_condutorRepository.UnitOfWork);
//        }

//        public async Task<ValidationResult> Handle(AtualizarUsuarioCommand message, CancellationToken cancellationToken)
//        {
//            if (!message.EhValido()) return message.ValidationResult;

//            var condutorExistente = await _condutorRepository.ObterPorId(message.Id);

//            if (condutorExistente == null)
//            {
//                AdicionarErro("Condutor não encontrado.");
//                return ValidationResult;
//            }

//            var condutor = new Condutor(message.Nome, condutorExistente.CPF, message.Telefone, message.Email, condutorExistente.CNH, message.DataNascimento)
//            {
//                Id = message.Id
//            };

//            _condutorRepository.Atualizar(condutor);

//            return await PersistirDados(_condutorRepository.UnitOfWork);
//        }

//        public async Task<ValidationResult> Handle(AtualizarVeiculoCondutorCommand message, CancellationToken cancellationToken)
//        {
//            if (!message.EhValido()) return message.ValidationResult;

//            var condutor = await _condutorRepository.ObterPorId(message.CondutorId);

//            if (condutor == null)
//            {
//                AdicionarErro("Condutor não encontrado.");
//                return ValidationResult;
//            }

//            _condutorRepository.AtualizarCondutorVeiculo(message.CondutorId, message.VeiculoId.ToString(), message.Placa);

//            return await PersistirDados(_condutorRepository.UnitOfWork);
//        }

//        public async Task<ValidationResult> Handle(ExcluirUsuarioCommand message, CancellationToken cancellationToken)
//        {
//            if (!message.EhValido()) return message.ValidationResult;

//            var condutor = await _condutorRepository.ObterPorId(message.Id);

//            if (condutor == null)
//            {
//                AdicionarErro("Condutor não encontrado.");
//                return ValidationResult;
//            }

//            if (condutor.Veiculo.Count > 0)
//            {
//                AdicionarErro("Necessário excluir os veículos cadastrados do condutor antes de excluí-lo.");
//                return ValidationResult;
//            }

//            _condutorRepository.Excluir(condutor);

//            return await PersistirDados(_condutorRepository.UnitOfWork);
//        }

//        public async Task<ValidationResult> Handle(ExcluirVeiculoCondutorCommand message, CancellationToken cancellationToken)
//        {
//            if (!message.EhValido()) return message.ValidationResult;

//            var veiculo = await _condutorRepository.ObterVeiculoId(message.VeiculoId);

//            if (veiculo == null)
//            {
//                AdicionarErro("Veículo não encontrado.");
//                return ValidationResult;
//            }

//            _condutorRepository.RemoverVeiculoCondutor(veiculo);

//            return await PersistirDados(_condutorRepository.UnitOfWork);
//        }
//    }
//}