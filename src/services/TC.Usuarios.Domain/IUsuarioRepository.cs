using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Domain
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<PagedResult<Usuario>> ObterTodos(int pageSize, int pageIndex, string query);
        Task<Usuario> ObterPorId(int id);
        Task<Usuario> ObterPorEmail(string email);
        void Adicionar(Usuario condutor);
        void Atualizar(Usuario condutor);
        void Excluir(Usuario condutor);
    }
}
