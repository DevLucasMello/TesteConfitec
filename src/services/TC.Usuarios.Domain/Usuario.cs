using System;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Nome Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public Escolaridade Escolaridade { get; private set; }

        public Usuario(Nome nome, string email, DateTime? dataNascimento, Escolaridade escolaridade)
        {
            Nome = nome;            
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        // EF Rel.
        protected Usuario() { }        

        public void MapearNome(string primeiroNome, string segundoNome)
        {
            Nome = new Nome(primeiroNome, segundoNome);
        }
    }
}