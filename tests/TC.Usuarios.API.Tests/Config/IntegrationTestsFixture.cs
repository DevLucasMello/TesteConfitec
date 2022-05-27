﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TC.Core.Tests.Config;
using TC.Core.Tests.Models;
using Xunit;

namespace TC.Usuarios.API.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {

        public string UsuarioToken;
        public UsuarioRespostaLogin UsuarioResponse;

        public readonly ApiConfigurationFactory<TStartup> _apiConfiguration;
        public HttpClient Client;

        public DesserializarObjeto _desserializar;

        public IntegrationTestsFixture()
        {
            _apiConfiguration = new ApiConfigurationFactory<TStartup>();
            Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5001/crud/")
            };
            UsuarioResponse = new UsuarioRespostaLogin();
            _desserializar = new DesserializarObjeto();
        }

        public async Task RealizarLoginApi()
        {
            var userData = new UsuarioLogin
            {
                Email = "teste@teste.com.br",
                Senha = "Teste@123"
            };

            var response = await Client.PostAsJsonAsync("autenticar", userData);
            response.EnsureSuccessStatusCode();
            UsuarioResponse = await _desserializar.DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            UsuarioToken = UsuarioResponse.AccessToken;
        }

        public void Dispose()
        {
            Client.Dispose();
            _apiConfiguration.Dispose();
        }
    }
}