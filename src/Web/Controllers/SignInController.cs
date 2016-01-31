using System.Threading.Tasks;
using System.Web.Http;

using Blog.Web.Constants;
using Blog.Web.Facade;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    [RoutePrefix(RoutePatterns.Prefix)]
    public class SignInController : Blog.Web.Infrastructure.ApiController
    {
        [Route(RoutePatterns.SignIn)]
        public async Task<SignInResponse> Post([FromBody]SignInRequest req)
        {
            return await this.GetService<SignInFacade>().Authenticate(req);
        }
    }
}