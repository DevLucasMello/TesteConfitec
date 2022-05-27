using System.Threading.Tasks;

namespace TC.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
