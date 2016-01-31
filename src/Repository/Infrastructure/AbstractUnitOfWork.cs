using System;
using System.Diagnostics;

using Blog.Repository.Infrastructure;

namespace Blog.Repository.Mock
{
    public abstract class AbstractUnitOfWork : IUnitOfWork
    {
        ~AbstractUnitOfWork()
        {
            this.Dispose(false);
        }

        public void Commit()
        {
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);
    }
}
