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

        public IUnitOfWork Create(string name = null)
        {
            // ctx = new DataContext(name);
            this.accessor.Context = name;
            return new UnitOfWork();
        }
    }
}