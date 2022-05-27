using System.Linq;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.Messages;
using TC.Usuarios.Application.Tests.Fixtures.Tests;
using Xunit;
namespace TC.Usuarios.Application.Tests.Command
{
    [Collection(nameof(UsuarioAutoMockerCollection))]
    public class AdicionarUsuarioCommandTests
    {
        private readonly UsuarioTestsAutoMockerFixture _usuarioTestsAutoMockerFixture;

        public AdicionarUsuarioCommandTests(UsuarioTestsAutoMockerFixture usuarioTestsFixture)
        {
            _usuarioTestsAutoMockerFixture = usuarioTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Usuario Comando Válido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Commands")]
        public void AdicionarUsuarioCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioValido();
            var usuarioCommand = new AdicionarUsuarioCommand(usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Usuario Comando Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Commands")]
        public void AdicionarCondutorCommand_ComandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuario = _usuarioTestsAutoMockerFixture.UsuarioInvalido();
            var usuarioCommand = new AdicionarUsuarioCommand(usuario.Nome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(UsuarioCommandErrorMessages.PrimeiroNomeNuloErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.UltimoNomeNuloErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));            
            Assert.Contains(UsuarioCommandErrorMessages.EmailNuloErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.EscolaridadeNuloErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.PrimeiroNomeQtdErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.UltimoNomeQtdErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));            
            Assert.Contains(UsuarioCommandErrorMessages.EmailInvalidoErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.DataNascimentoMaiorDataAtualErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(UsuarioCommandErrorMessages.EscolaridadeNaoPermitidaErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}