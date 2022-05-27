using System.Linq;
using System.Threading.Tasks;
using Moq;
using TC.Usuarios.Application.Queries;
using TC.Usuarios.Application.Tests.Fixtures.Tests;
using Xunit;

namespace TC.Usuarios.Application.Tests.Queries
{
    [Collection(nameof(UsuarioAutoMockerCollection))]
    public class CondutorQueriesTests
    {
        private readonly UsuarioTestsAutoMockerFixture _usuarioTestsAutoMockerFixture;
        private readonly UsuarioQueries _usuarioQueries;
        private readonly int pageSize;
        private readonly int pageIndex;
        private readonly string query;

        public CondutorQueriesTests(UsuarioTestsAutoMockerFixture usuarioTestsFixture)
        {
            _usuarioTestsAutoMockerFixture = usuarioTestsFixture;
            _usuarioQueries = _usuarioTestsAutoMockerFixture.ObterUsuarioQueries();
            pageSize = 8;
            pageIndex = 1;
            query = null;
        }

        [Fact(DisplayName = "Obter Todos Usuarios")]
        [Trait("Categoria", "UsuariosAPI - Usuario Queries")]
        public async void ObterTodosUsuairosQuery_QueryValida_DeveRetornarListaUsuarios()
        {
            // Arrange
            var usuarios = _usuarioTestsAutoMockerFixture.ObterUsuariosPaginados();

            _usuarioTestsAutoMockerFixture._usuarioRepository.Setup(c => c.ObterTodos(pageSize, pageIndex, query))
                .Returns(Task.FromResult(usuarios));

            // Act
            var condutoresQuery = await _usuarioQueries.ObterTodosUsuarios(pageSize, pageIndex, query);

            // Assert
            _usuarioTestsAutoMockerFixture._usuarioRepository.Verify(r => r.ObterTodos(pageSize, pageIndex, query), Times.Once);
            Assert.True(condutoresQuery.List.Any());
        }
    }
}