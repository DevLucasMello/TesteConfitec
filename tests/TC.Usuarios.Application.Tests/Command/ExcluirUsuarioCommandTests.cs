using System;
using System.Linq;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.Messages;
using Xunit;

namespace TC.Usuarios.Application.Tests.Command
{
    public class ExcluirUsuarioCommandTests
    {
        [Fact(DisplayName = "Excluir Usuario Comando Válido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Commands")]
        public void ExcluirUsuarioCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new ExcluirUsuarioCommand(1);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Excluir Usuario Comando Inválido")]
        [Trait("Categoria", "UsuariosAPI - Usuario Commands")]
        public void ExcluirUsuarioCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var usuarioCommand = new ExcluirUsuarioCommand(0);

            // Act
            var result = usuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(UsuarioCommandErrorMessages.IdNuloErroMsg, usuarioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
