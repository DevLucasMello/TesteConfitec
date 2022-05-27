using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using Moq;
using Moq.AutoMock;
using TC.Usuarios.Application.AutoMapper;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.Queries;
using TC.Usuarios.Domain;
using TC.Core.DomainObjects;
using Xunit;

namespace TC.Usuarios.Application.Tests.Fixtures.Tests
{
    [CollectionDefinition(nameof(UsuarioAutoMockerCollection))]
    public class UsuarioAutoMockerCollection : ICollectionFixture<UsuarioTestsAutoMockerFixture>
    {
    }

    public class UsuarioTestsAutoMockerFixture : IDisposable
    {       
        private UsuarioCommandHandler _usuarioHandler;
        private UsuarioQueries _usuarioQueries;
        public AutoMocker _mocker;
        private Mapper _mapper;
        public Mock<IUsuarioRepository> _usuarioRepository;        

        public Usuario UsuarioValido()
        {
            return GerarUsuarios(1).FirstOrDefault();
        }

        public Usuario UsuarioInvalido()
        {
            return GerarUsuarioInvalido();
        }

        public PagedResult<Usuario> ObterUsuariosPaginados()
        {
            var usuarios = new PagedResult<Usuario>
            {
                List = GerarUsuarios(20)
            };

            return usuarios;
        }

        public IEnumerable<Usuario> ObterUsuarios()
        {
            var usuarios = new List<Usuario>();

            usuarios.AddRange(GerarUsuarios(20).ToList());

            return usuarios.AsEnumerable();
        }

        private IEnumerable<Usuario> GerarUsuarios(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var usuarios = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    new Nome(f.Name.FirstName(genero), f.Name.LastName(genero)),
                    "",
                    f.Date.Past(80, DateTime.Now.AddYears(-1)),
                    default))
                                
                .RuleFor(c => c.Email, (f, c) =>
                      f.Internet.Email(c.Nome.PrimeiroNome.ToLower(), c.Nome.UltimoNome.ToLower()))
                .RuleFor(p => p.Escolaridade, f => f.Random.Enum<Escolaridade>());
                

            return usuarios.Generate(quantidade);
        }

        private Usuario GerarUsuarioInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var usuario = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    new Nome("", ""), "", null, default))

                .RuleFor(p => p.DataNascimento, f => f.Date.Past(0, DateTime.Now.AddYears(+1)));

            return usuario;
        }

        public UsuarioCommandHandler ObterUsuarioHandler()
        {
            _mocker = new AutoMocker();
            _usuarioHandler = _mocker.CreateInstance<UsuarioCommandHandler>();

            return _usuarioHandler;
        }

        public UsuarioQueries ObterUsuarioQueries()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioQueries = new UsuarioQueries(_usuarioRepository.Object, ObterUsuarioMapper());            

            return _usuarioQueries;
        }

        public Mapper ObterUsuarioMapper()
        {
            var myProfile = new ExibirUsuarioQuerieToViewModel();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            return _mapper;
        }

        public void Dispose()
        {
        }
    }
}