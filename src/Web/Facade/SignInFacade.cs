using System.Threading.Tasks;

using Blog.Repository.Infrastructure;
using Blog.Service.Interface;
using Blog.Web.Models;

namespace Blog.Web.Facade
{
    /// <summary>
    /// Acts as Application Service. Encapsulates use case and simplifies interface.
    /// Used by web API controller.
    /// Uses Domain Services in scope of Unit of Work.
    /// </summary>
    public sealed class SignInFacade
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IUserService userService;

        public SignInFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.userService = userService;
        }

        public async Task<SignInResponse> Authenticate(SignInRequest req)
        {
            if (req == null)
            {
                return null;
            }

            bool succeed;
            using (var unitOfWork = this.unitOfWorkProvider.Create("ro"))
            { 
                succeed = await this.userService.Authenticate(req.Username, req.Password);                
            }

            if (!succeed)
            {
                return null;
            }

            return new SignInResponse { Username = req.Username };
        }
    }
}