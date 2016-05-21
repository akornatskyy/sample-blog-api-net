using System.Data;
using System.Threading.Tasks;

namespace Blog.Repository.Infrastructure
{
    public interface IUnitOfWorkProvider
    {
        Task<IUnitOfWork> Create(string name, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}