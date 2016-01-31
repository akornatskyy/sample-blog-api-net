namespace Blog.Repository.Infrastructure
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Create(string name = null);
    }
}
