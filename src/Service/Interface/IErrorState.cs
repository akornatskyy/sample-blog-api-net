namespace Blog.Service.Interface
{
    public interface IErrorState
    {
        bool HasErrors { get; }

        void AddError(string message);
        
        void AddError(string key, string message);
    }
}