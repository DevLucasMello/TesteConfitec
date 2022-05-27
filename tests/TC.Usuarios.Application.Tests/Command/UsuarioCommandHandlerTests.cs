using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.Messages;
using TC.Usuarios.Application.Tests.Fixtures.Tests;
using TC.Usuarios.Domain;
using Xunit;

namespace TC.Usuarios.Application.Tests.Command
{
    [Collection(nameof(UsuarioAutoMockerCollection))]
    public class UsuarioCommandHandlerTests
    {
        private readonly UsuarioTestsAutoMockerFixture _usuarioTestsAutoMockerFixture;
        private UsuarioCommandHandler _usuarioHandler;

        public UsuarioCommandHandlerTests(UsuarioTestsAutoMockerFixture usuarioTestsFixture)
        {
            _usuarioTestsAutoMockerFixture = usuarioTestsFixture;
            _usuarioHandler = _usuarioTestsAutoMockerFixture.ObterUsuarioHandler();
        }

        #region AdicionarUsuarioCommand

        [Fact(DisplayName = "Adicionar Novo Usuario com Sucesso")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task AdicionarUsuarioCommand_NovoCondutor_DeveExecutarComSucesso()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            var usuarioCommand = new AdicionarUsuarioCommand(usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.Adicionar(It.IsAny<Usuario>()), Times.Once);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Novo Usuario Command Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task AdicionarUsuarioCommand_CommandoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioInvalido();
            var usuarioCommand = new AdicionarUsuarioCommand(usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        #endregion        

        #region AtualizarUsuarioCommand

        [Fact(DisplayName = "Atualizar Usuario Command Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task AtualizarUsuarioCommand_CommandoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioInvalido();
            var usuarioCommand = new AtualizarUsuarioCommand(0, usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Atualizar Usuario com Sucesso")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task AtualizarUsuarioCommand_CommandoValido_DeveExecutarComSucesso()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            usuario.Id = 1;            

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterPorId(usuario.Id)).
                Returns(Task.FromResult(usuario));

            var usuarioCommand = new AtualizarUsuarioCommand(1, usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.Atualizar(It.IsAny<Usuario>()), Times.Once);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Usuario Obter Usuario com Id Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task AtualizarUsuarioCommand_IdInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            usuario.Id = 1;

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterPorId(2)).
                Returns(Task.FromResult(usuario));

            var usuarioCommand = new AtualizarUsuarioCommand(usuario.Id, usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterPorId(usuario.Id), Times.Once);
            Assert.Contains(UsuarioCommandErrorMessages.UsuarioNaoEncontradoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }

        #endregion

        #region ExcluirUsuarioCommand

        [Fact(DisplayName = "Excluir Usuario Command Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task ExcluirUsuarioCommand_CommandoInvalido_DeveRetornarFalso()
        {
            // Arrange
            var usuarioCommand = new ExcluirUsuarioCommand(0);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Excluir Usuario Command com Sucesso")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task ExcluirUsuarioCommand_CommandoValido_DeveExecutarComSucesso()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            usuario.Id = 1;

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterPorId(usuario.Id)).
                Returns(Task.FromResult(usuario));

            var usuarioCommand = new ExcluirUsuarioCommand(usuario.Id);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.Excluir(It.IsAny<Usuario>()), Times.Once);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Excluir Usuario Obter Usuario com Id Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Command Handler")]
        public async Task ExcluirUsuarioCommand_IdInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            usuario.Id = 1;

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterPorId(2)).
                Returns(Task.FromResult(usuario));

            var usuarioCommand = new ExcluirUsuarioCommand(usuario.Id);

            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _usuarioHandler.Handle(usuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _usuarioTestsAutoMockerFixture._mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterPorId(usuario.Id), Times.Once);
            Assert.Contains(UsuarioCommandErrorMessages.UsuarioNaoEncontradoErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }

        #endregion
    }
}