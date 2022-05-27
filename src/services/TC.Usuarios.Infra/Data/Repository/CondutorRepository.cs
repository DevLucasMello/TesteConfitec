using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TC.Usuarios.Domain;
using TC.Core.Data;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext _context;

        public UsuarioRepository(UsuariosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PagedResult<Usuario>> ObterTodos(int pageSize, int pageIndex, string query)
        {
            var sql = @$"SELECT u.Id,c.Email, u.DataNascimento, u.Escolaridade, u.PrimeiroNome, u.UltimoNome
                      FROM Usuario u
                      WHERE (@Nome IS NULL OR PrimeiroNome LIKE '%' + @Nome + '%') 
                      ORDER BY [PrimeiroNome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Usuario 
                      WHERE (@Nome IS NULL OR PrimeiroNome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });
            
            var usuarios = multi.Read<Usuario, Nome, Usuario>((u, n) => {
                u.MapearNome(n.PrimeiroNome, n.UltimoNome);
                return u;
            }, "PrimeiroNome");

            var total = multi.Read<int>().FirstOrDefault();            

            return new PagedResult<Usuario>()
            {
                List = usuarios,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }        

        public async Task<Usuario> ObterPorId(int id)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Usuario> ObterPorEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void Excluir(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
