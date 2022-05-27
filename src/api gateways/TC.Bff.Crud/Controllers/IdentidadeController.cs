using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TC.Bff.Crud.Models.Identidade;
using TC.Bff.Crud.Services;
using TC.WebAPI.Core.Controllers;

namespace TC.Bff.Crud.Controllers
{
    public class IdentidadeController : MainController
    {
        private readonly IIdentidadeService _identidadeService;

        public IdentidadeController(IIdentidadeService identidadeService)
        {
            _identidadeService = identidadeService;
        }

        [HttpPost]
        [Route("crud/nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistroDTO usuarioRegistro)
        {
            return CustomResponse(await _identidadeService.Registrar(usuarioRegistro));
        }

        [HttpPost]
        [Route("crud/autenticar")]
        public async Task<ActionResult> Login(UsuarioLoginDTO usuarioLogin)
        {
            return CustomResponse(await _identidadeService.Login(usuarioLogin));
        }
    }
}