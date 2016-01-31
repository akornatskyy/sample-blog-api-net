using System.Threading.Tasks;

using Blog.Models;

namespace Blog.Repository.Interface
{
    public interface IUserRepository
    {
        Task<AuthInfo> FindAuthInfo(string username);
    }
}