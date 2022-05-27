using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TC.Usuarios.Application.Events
{
    public class UsuarioEventHandler : INotificationHandler<UsuarioRegistradoEvent>
    {
        public Task Handle(UsuarioRegistradoEvent message, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}