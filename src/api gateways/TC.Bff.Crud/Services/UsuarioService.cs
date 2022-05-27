using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TC.Bff.Crud.Extensions;
using TC.Bff.Crud.Models.Usuario;
using TC.Core.Communication;
using TC.Core.DomainObjects;
using TC.WebAPI.Core.Http;

namespace TC.Bff.Crud.Services
{
    public interface IUsuarioService
    {
        Task<PagedResult<UsuarioDTO>> ObterTodosUsuarios(int pageSize, int pageIndex, string query);
        Task<UsuarioDTO> ObterUsuarioPorId(int id);
        Task<UsuarioDTO> ObterUsuarioPorEmail(string email);
        Task<ResponseResult> AdicionarUsuario(AdicionarUsuarioDTO usuario);
        Task<ResponseResult> AtualizarUsuario(int id, AtualizarUsuarioDTO usuario);
        Task<ResponseResult> ExcluirUsuario(int id);

    }

    public class UsuarioService : Service, IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.UsuarioUrl);
        }

        public async Task<PagedResult<UsuarioDTO>> ObterTodosUsuarios(int pageSize, int pageIndex, string query)
        {
            var response = await _httpClient.GetAsync($"/usuario?ps={pageSize}&page={pageIndex}&q={query}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<PagedResult<UsuarioDTO>>(response);
        }

        public async Task<UsuarioDTO> ObterUsuarioPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/usuario/{id}");           

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<UsuarioDTO>(response);
        }

        public async Task<UsuarioDTO> ObterUsuarioPorEmail(string email)
        {
            var response = await _httpClient.GetAsync($"/usuario/email/{email}");

            if (!TratarErrosResponse(response)) return null;

            return await DeserializarObjetoResponse<UsuarioDTO>(response);
        }

        public async Task<ResponseResult> AdicionarUsuario(AdicionarUsuarioDTO usuario)
        {
            var itemContent = ObterConteudo(usuario);

            var response = await _httpClient.PostAsync("/usuario/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> AtualizarUsuario(int id, AtualizarUsuarioDTO usuario)
        {
            var itemContent = ObterConteudo(usuario);

            var response = await _httpClient.PutAsync($"/usuario/{id}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }

        public async Task<ResponseResult> ExcluirUsuario(int id)
        {
            var response = await _httpClient.DeleteAsync($"/usuario/{id}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk(response);
        }
    }
}
