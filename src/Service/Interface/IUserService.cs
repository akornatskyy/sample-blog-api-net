using System.Threading.Tasks;

namespace Blog.Service.Interface
{
    public interface IUserService
    {
        Task<bool> Authenticate(string username, string password);
    }
}