using System.Threading.Tasks;
using Blog.Repository.Infrastructure;

namespace Blog.Repository.Mock
{
    public sealed class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly ContextAccessor accessor;

        public UnitOfWorkProvider(ContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public Task<IUnitOfWork> Create(string name = null)
        {
            // ctx = new DataContext(name);
            this.accessor.Context = name;
            return Task.FromResult<IUnitOfWork>(new UnitOfWork());
        }
    }
}