using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.Queries;
using TC.Usuarios.Application.ViewModels;
using TC.Core.Mediator;
using TC.WebAPI.Core.Controllers;

namespace TC.Usuario.API.Controllers
{
    [Authorize]
    public class UsuarioController : MainController
    {
        private readonly IMediatorHandler _mediator;        
        private readonly IMapper _mapper;
        private readonly IUsuarioQueries _usuarioQueries;

        public UsuarioController(IMediatorHandler mediator, IMapper mapper, IUsuarioQueries usuarioQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _usuarioQueries = usuarioQueries;
        }

        [HttpGet("usuario")]
        public async Task<IActionResult> ObterTodosUsuarios([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var usuarios = await _usuarioQueries.ObterTodosUsuarios(ps, page, q);

            return !usuarios.List.Any() ? NotFound() : CustomResponse(usuarios);
        }

        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioQueries.ObterUsuarioPorId(id);

            return usuario == null ? NotFound() : CustomResponse(usuario);
        }

        [HttpGet("usuario/email/{email}")]
        public async Task<IActionResult> ObterUsuarioPorCpf(string email)
        {
            var usuario = await _usuarioQueries.ObterUsuarioPorEmail(email);

            return usuario == null ? NotFound() : CustomResponse(usuario);
        }

        [HttpPost("usuario")]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioViewModel usuarioViewModel)
        {
            var usuario = _mapper.Map<AdicionarUsuarioCommand>(usuarioViewModel);
            return CustomResponse(await _mediator.EnviarComando(usuario));
        }

        [HttpPut("usuario/{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, AtualizarUsuarioViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.Id) return NotFound();
            var usuario = _mapper.Map<AtualizarUsuarioCommand>(usuarioViewModel);
            return CustomResponse(await _mediator.EnviarComando(usuario));
        }

        [HttpDelete("usuario/{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {            
            return CustomResponse(await _mediator.EnviarComando(new ExcluirUsuarioCommand(id)));
        }
    }
}
