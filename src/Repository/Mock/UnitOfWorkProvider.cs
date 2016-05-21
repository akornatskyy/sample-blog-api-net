using System;
using System.Data;
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

        public Task<IUnitOfWork> Create(string name, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return Task.FromResult<IUnitOfWork>(new UnitOfWork(name, this.accessor));
        }
    }
}