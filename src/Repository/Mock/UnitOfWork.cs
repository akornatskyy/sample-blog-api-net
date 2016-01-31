using System;
using System.Diagnostics;

using Blog.Repository.Infrastructure;

namespace Blog.Repository.Mock
{
    public sealed class UnitOfWork : AbstractUnitOfWork
    {
        protected override void Dispose(bool disposing)
        { 
        }
    }
}
