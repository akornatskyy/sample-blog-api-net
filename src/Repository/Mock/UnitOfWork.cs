using Blog.Repository.Infrastructure;

namespace Blog.Repository.Mock
{
    public sealed class UnitOfWork : AbstractUnitOfWork
    {
        private readonly ContextAccessor accessor;

        public UnitOfWork(string name, ContextAccessor accessor)
        {
            accessor.Context = name;
            this.accessor = accessor;
        }

        public override void Commit()
        {
            this.accessor.Context = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.accessor.Context = null;
            }
        }
    }
}