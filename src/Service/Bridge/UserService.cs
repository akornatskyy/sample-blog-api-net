using System.Threading.Tasks;

using Blog.Repository.Interface;
using Blog.Service.Interface;

namespace Blog.Service.Bridge
{
    /// <summary>
    /// Acts as Domain Service. Encapsulates business logic.
    /// Used by Application Services (Facade).
    /// Uses micro operations of repositories to acomplish business operation.
    /// </summary>
    public sealed class UserService : IUserService
    {
        private readonly IErrorState errorState;
        private readonly IUserRepository userRepository;

        public UserService(IErrorState errorState, IUserRepository userRepository)
        {
            this.errorState = errorState;
            this.userRepository = userRepository;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var authInfo = await this.userRepository.FindAuthInfo(username.ToLowerInvariant());
            if (authInfo == null || authInfo.Password != password)
            {
                this.errorState.AddError(Properties.Resources.SignInFailed);
                return false;
            }

            if (authInfo.IsLocked)
            {
                this.errorState.AddError(Properties.Resources.UserLocked);
                return false;
            }
            
            return true;
        }
    }
}
