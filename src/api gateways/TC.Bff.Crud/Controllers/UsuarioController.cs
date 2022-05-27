using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TC.Bff.Crud.Models.Usuario;
using TC.Bff.Crud.Services;
using TC.WebAPI.Core.Controllers;

namespace TC.Bff.Crud.Controllers
{
    [Authorize]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("crud/usuario")]
        public async Task<IActionResult> ObterTodosUsuarios([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return CustomResponse(await _usuarioService.ObterTodosUsuarios(ps, page, q));
        }
        

        [HttpGet]
        [Route("crud/usuario/{id}")]
        public async Task<IActionResult> ObterUsuarioPorId(int id)
        {
            return CustomResponse(await _usuarioService.ObterUsuarioPorId(id));
        }

        [HttpGet]
        [Route("crud/usuario/email/{email}")]
        public async Task<IActionResult> ObterUsuarioPorEmail(string email)
        {
            return CustomResponse(await _usuarioService.ObterUsuarioPorEmail(email));
        }

        [HttpPost]
        [Route("crud/usuario")]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioDTO usuario)
        {
            return CustomResponse(await _usuarioService.AdicionarUsuario(usuario));            
        }

        [HttpPut]
        [Route("crud/usuario/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = await _usuarioService.ObterUsuarioPorId(id);
            if (usuarioExistente is null)
            {
                AdicionarErroProcessamento("Usuario não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _usuarioService.AtualizarUsuario(id, usuario));
        }

        [HttpDelete]
        [Route("crud/usuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            var usuarioExistente = await _usuarioService.ObterUsuarioPorId(id);
            if (usuarioExistente is null)
            {
                AdicionarErroProcessamento("Usuario não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(await _usuarioService.ExcluirUsuario(id));
        }
    }
}