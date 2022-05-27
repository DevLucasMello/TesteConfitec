using System.Net.Http.Json;
using System.Threading.Tasks;
using TC.Usuarios.API.Tests.Config;
using TC.Usuarios.Application.ViewModels;
using TC.Core.Tests.Config;
using Xunit;
using TC.Core.DomainObjects;

namespace TC.Usuarios.API.Tests
{
    [TestCaseOrderer("TC.Usuarios.API.Tests.PriorityOrderer", "TC.Usuarios.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class UsuariosApiTests
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        private readonly int pageSize;
        private readonly int pageIndex;
        private readonly string query;

        public UsuariosApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
            pageSize = 8;
            pageIndex = 1;
            query = null;
        }

        [Fact(DisplayName = "Adicionar Novo Usuario"), TestPriority(1)]
        [Trait("Categoria", "UsuariosAPI - Integração")]
        public async Task UsuariosApi_AdicionarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            var usuario = new AdicionarUsuarioViewModel
            {
                PrimeiroNome = "Teste",
                UltimoNome = "Teste Sobrenome",
                Email = "teste@teste.com.br",
                DataNascimento = "02/02/1990",
                Escolaridade = Escolaridade.Superior.ToString()
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("usuario", usuario);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter Todos Usuarios"), TestPriority(2)]
        [Trait("Categoria", "UsuariosAPI - Integração")]
        public async Task UsuariosApi_ObterTodosUsuarios_DeveRetornarComSucesso()
        {
            // Arrange            
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync($"usuario?ps={pageSize}&page={pageIndex}&q={query}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Obter Usuarios por Email"), TestPriority(3)]
        [Trait("Categoria", "UsuariosAPI - Integração")]
        public async Task UsuariosApi_ObterUsuariosPorEmail_DeveRetornarComSucesso()
        {
            // Arrange
            string email = "teste@teste.com.br";
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var getResponse = await _testsFixture.Client.GetAsync($"usuario/email/{email}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Atualizar Usuario"), TestPriority(4)]
        [Trait("Categoria", "UsuariosAPI - Integração")]
        public async Task UsuariosApi_AtualizarUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            string email = "teste@teste.com.br";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var getResponse = await _testsFixture.Client.GetAsync($"usuario/email/{email}");
            var usuario = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirUsuarioViewModel>(getResponse);
            usuario.Id = 1;

            var usuarioAtualizadar = new AtualizarUsuarioViewModel
            {
                Id = usuario.Id,
                PrimeiroNome = "Lucas",
                UltimoNome = "Santos",                
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                Escolaridade = usuario.Escolaridade
            };

            // Act
            var postResponse = await _testsFixture.Client.PutAsJsonAsync($"usuario/{usuarioAtualizadar.Id}", usuario);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Excluir Usuario"), TestPriority(5)]
        [Trait("Categoria", "UsuariosAPI - Integração")]
        public async Task UsuariosApi_ExcluirUsuario_DeveRetornarComSucesso()
        {
            // Arrange
            string email = "teste@teste.com.br";

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var getResponse = await _testsFixture.Client.GetAsync($"usuario/email/{email}");
            var usuario = await _testsFixture._desserializar.DeserializarObjetoResponse<ExibirUsuarioViewModel>(getResponse);
            usuario.Id = 1;

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"usuario/{usuario.Id}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}