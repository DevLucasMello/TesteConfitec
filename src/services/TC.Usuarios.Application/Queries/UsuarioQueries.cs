using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TC.Usuarios.Application.ViewModels;
using TC.Usuarios.Domain;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Application.Queries
{
    public interface IUsuarioQueries
    {
        Task<PagedResult<ExibirUsuarioViewModel>> ObterTodosUsuarios(int pageSize, int pageIndex, string query);
        Task<ExibirUsuarioViewModel> ObterUsuarioPorId(int id);
        Task<ExibirUsuarioViewModel> ObterUsuarioPorEmail(string email);
    }

    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioQueries(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ExibirUsuarioViewModel>> ObterTodosUsuarios(int pageSize, int pageIndex, string query)
        {
            var usuarios = await _usuarioRepository.ObterTodos(pageSize, pageIndex, query);

            if (usuarios == null)
                return null;

            var result = new PagedResult<ExibirUsuarioViewModel>()
            {
                List = _mapper.Map<IEnumerable<ExibirUsuarioViewModel>>(usuarios.List),
                TotalResults = usuarios.TotalResults,
                PageIndex = usuarios.PageIndex,
                PageSize = usuarios.PageSize,
                Query = usuarios.Query
            };

            return result;
        }        

        public async Task<ExibirUsuarioViewModel> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                return null;

            return _mapper.Map<ExibirUsuarioViewModel>(usuario);
        }

        public async Task<ExibirUsuarioViewModel> ObterUsuarioPorEmail(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);

            if (usuario == null)
                return null;

            return _mapper.Map<ExibirUsuarioViewModel>(usuario);
        }
    }
}