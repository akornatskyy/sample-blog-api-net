using System;

namespace Blog.Repository.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}