using TC.Core.DomainObjects;
using TC.Core.Messages;

namespace TC.Usuarios.Application.Events
{
    public class UsuarioRegistradoEvent : Event
    {
        public Nome Nome { get; private set; }
        public string Email { get; private set; }

        public UsuarioRegistradoEvent(Nome nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
